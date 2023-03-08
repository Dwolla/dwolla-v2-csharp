using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dwolla.Client.Models.Responses
{
    public class GetLabelLedgerEntriesResponse : BaseGetResponse<LabelLedgerEntry>
    {
        [JsonPropertyName("_embedded")]
        public new LabelLedgerEntriesEmbed Embedded { get; set; }
    }

    public class LabelLedgerEntriesEmbed : Embed<LabelLedgerEntry>
    {
        [JsonPropertyName("ledger-entries")]
        public List<LabelLedgerEntry> LedgerEntries { get; set; }

        public override List<LabelLedgerEntry> Results() => LedgerEntries;
    }
}