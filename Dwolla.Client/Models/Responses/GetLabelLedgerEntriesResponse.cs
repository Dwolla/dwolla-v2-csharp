using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Responses
{
    public class GetLabelLedgerEntriesResponse : BaseGetResponse<LabelLedgerEntry>
    {
        [JsonProperty(PropertyName = "_embedded")]
        public new LabelLedgerEntriesEmbed Embedded { get; set; }
    }

    public class LabelLedgerEntriesEmbed : Embed<LabelLedgerEntry>
    {
        [JsonProperty(PropertyName = "ledger-entries")]
        public List<LabelLedgerEntry> LedgerEntries { get; set; }

        public override List<LabelLedgerEntry> Results() => LedgerEntries;
    }
}