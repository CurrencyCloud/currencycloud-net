using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class ReportRequest : Entity
    {
        [JsonConstructor]
        public ReportRequest() { }

        public string Id { get; set; }

        public string ShortReference { get; set; }

        public string Description { get; set; }

        public ReportParameters SearchParams { get; set; }

        public string ReportType { get; set; }

        public string Status { get; set; }

        public string FailureReason { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public string ReportUrl { get; set; }

        public string AccountId { get; set; }

        public string ContactId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Id,
                    ShortReference,
                    Description,
                    SearchParams,
                    ReportType,
                    Status,
                    FailureReason,
                    ExpirationDate,
                    ReportUrl,
                    AccountId,
                    ContactId,
                    CreatedAt,
                    UpdatedAt
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ReportRequest))
            {
                return false;
            }

            var reportRequest = obj as ReportRequest;

            return Id == reportRequest.Id &&
                   ShortReference == reportRequest.ShortReference &&
                   Description == reportRequest.Description &&
                   SearchParams == reportRequest.SearchParams &&
                   ReportType == reportRequest.ReportType &&
                   Status == reportRequest.Status &&
                   FailureReason == reportRequest.FailureReason &&
                   ExpirationDate == reportRequest.ExpirationDate &&
                   ReportUrl == reportRequest.ReportUrl &&
                   AccountId == reportRequest.AccountId &&
                   ContactId == reportRequest.ContactId &&
                   CreatedAt == reportRequest.CreatedAt &&
                   UpdatedAt == reportRequest.UpdatedAt;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}