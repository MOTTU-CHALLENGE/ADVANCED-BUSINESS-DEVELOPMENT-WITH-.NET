using CM_API_MVC.Models;
using Microsoft.ML;

namespace CM_API_MVC.Services
{
    public class PreditorPosicaoService
    {
        private readonly string _caminhoModelo = "Data/modelo-posicao.zip";
        private readonly MLContext _mlContext;
        private PredictionEngine<EntradaPosicao, PredicaoPosicao>? _engine;

        public PreditorPosicaoService()
        {
            _mlContext = new MLContext();

            if (File.Exists(_caminhoModelo))
            {
                var model = _mlContext.Model.Load(_caminhoModelo, out _);
                _engine = _mlContext.Model.CreatePredictionEngine<EntradaPosicao, PredicaoPosicao>(model);
            }
        }
        public void TreinarComCsv()
        {
            var caminhoCsv = "Data/T_DF_WIFI_RANGE.csv";

            var data = _mlContext.Data.LoadFromTextFile<DadosTreinoPosicao>(
                path: caminhoCsv,
                hasHeader: true,
                separatorChar: ',');

            var pipeline = _mlContext.Transforms
                .Text.FeaturizeText("BssidFeaturized", "Bssid") 
                .Append(_mlContext.Transforms.Concatenate("Features", "IdDispositivo", "BssidFeaturized", "Rssi"))
                .Append(_mlContext.Transforms.CopyColumns("Label", "CoordenadaX"))
                .Append(_mlContext.Regression.Trainers.Sdca(labelColumnName: "Label"));


            var model = pipeline.Fit(data);

            var caminhoSalvamento = Path.Combine("/home/data", "modelo-posicao.zip");
            _mlContext.Model.Save(model, data.Schema, caminhoSalvamento);

        }


        public PredicaoPosicao Prever(EntradaPosicao entrada)
        {
            if (_engine == null) throw new Exception("Modelo não treinado.");

            var resultado = _engine.Predict(entrada);

            return new PredicaoPosicao
            {
                CoordenadaX = Math.Round(resultado.CoordenadaX, 2)
            };
        }
    }
}
