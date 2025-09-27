using CM_API_MVC.Dtos.Link;
using static System.Net.WebRequestMethods;

namespace CM_API_MVC.Dtos.Moto
{
    public class MotoLinksHelper
    {
        private readonly LinkGenerator _linkGenerator;

        public MotoLinksHelper(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        public MotoHatDto AddLinks(MotoDto moto, HttpContext http)
        {
            var dto = new MotoHatDto
            {
                IdMoto = moto.IdMoto,
                CodTag = moto.CodTag,
                TipoMoto = moto.TipoMoto,
                Placa = moto.Placa,
                Status = moto.Status,
                DataCadastro = moto.DataCadastro,
                AnoFabricacao = moto.AnoFabricacao,
                Modelo = moto.Modelo,
            };

            var id = dto.IdMoto;

            dto.Links.Add(new LinkDto
            {
                Rel = "self",
                Href = _linkGenerator.GetPathByAction(http, "GetById", "MotoApi", new { id }),
                Method = "GET"
            });

            dto.Links.Add(new LinkDto
            {
                Rel = "update",
                Href = _linkGenerator.GetPathByAction(http, "Update", "MotoApi", new { id }),
                Method = "PUT"
            });

            dto.Links.Add(new LinkDto
            {
                Rel = "delete",
                Href = _linkGenerator.GetPathByAction(http, "Delete", "MotoApi", new { id }),
                Method = "DELETE"
            });

            return dto;

        }
    }
}
