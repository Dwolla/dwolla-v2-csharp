using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.BusinessClassifications
{
    [Task("lbc", "List Business Classifications")]
    internal class List : BaseTask
    {
        public override async Task Run()
        {
            var response = await HttpService.BusinessClassification.GetBusinessClassificationCollectionAsync();

            response.Content.Embedded.BusinessClassifications
                .ForEach(bc => bc.Embedded.IndustryClassifications
                    .ForEach(ic => WriteLine($"{bc.Id}:{bc.Name} - {ic.Id}:{ic.Name}")));
        }
    }
}