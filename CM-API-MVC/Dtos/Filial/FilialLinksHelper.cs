using CM_API_MVC.Dtos.Link;

namespace CM_API_MVC.Dtos.Filial
{
    public class FilialLinksHelper
    {
        private readonly LinkGenerator _linkGenerator;

        public FilialLinksHelper(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        public FilialComPatioHatDto AddLinks(FilialComPatioDto filial, HttpContext http)
        {
            var dto = new FilialComPatioHatDto
            {
                IdFilial = filial.IdFilial,
                NomeFilial = filial.NomeFilial,
                Endereco = filial.Endereco,
                Cidade = filial.Cidade,
                Estado = filial.Estado,
                Pais = filial.Pais,
                Cep = filial.Cep,
                Telefone = filial.Telefone,
                DataInauguracao = filial.DataInauguracao,
                Patios = filial.Patios,

            };

            var id = filial.IdFilial;

            dto.Links.Add(new LinkDto
            {
                Rel = "self",
                Href = _linkGenerator.GetPathByAction(http, "GetById", "FilialApi", new { id }),
                Method = "GET"
            });

            dto.Links.Add(new LinkDto
            {
                Rel = "update",
                Href = _linkGenerator.GetPathByAction(http, "Update", "FilialApi", new { id }),
                Method = "PUT"
            });

            dto.Links.Add(new LinkDto
            {
                Rel = "delete",
                Href = _linkGenerator.GetPathByAction(http, "Delete", "FilialApi", new { id }),
                Method = "DELETE"
            });

            return dto;
        }
    }

}
