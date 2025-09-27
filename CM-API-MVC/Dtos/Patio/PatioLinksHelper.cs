using CM_API_MVC.Dtos.Link;

namespace CM_API_MVC.Dtos.Patio
{
    public class PatioLinksHelper
    {
        private readonly LinkGenerator _linkGenerator;

        public PatioLinksHelper(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        public PatioComWifiHatDto AddLinks(PatioComWifiDto patio, HttpContext http)
        {
            var dto = new PatioComWifiHatDto
            {
                IdPatio = patio.IdPatio,
                IdFilial = patio.IdFilial,
                NomePatio = patio.NomePatio,
                CapacidadeMax = patio.CapacidadeMax,
                Area = patio.Area,
                Descricao = patio.Descricao,
                ReceptorWifi = patio.ReceptorWifi,

            };

            var id = dto.IdPatio;

            dto.Links.Add(new LinkDto
            {
                Rel = "self",
                Href = _linkGenerator.GetPathByAction(http, "GetById", "PatioApi", new { id }),
                Method = "GET"
            });

            dto.Links.Add(new LinkDto
            {
                Rel = "update",
                Href = _linkGenerator.GetPathByAction(http, "Update", "PatioApi", new { id }),
                Method = "PUT"
            });

            dto.Links.Add(new LinkDto
            {
                Rel = "delete",
                Href = _linkGenerator.GetPathByAction(http, "Delete", "PatioApi", new { id }),
                Method = "DELETE"
            });

            return dto;

        }
    }

}
