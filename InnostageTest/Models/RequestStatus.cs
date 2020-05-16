using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InnostageTest.Models
{
    public class RequestStatus
    {
        public int RequestStatusId { get; set; }

        [Display(Name = "Статус")]
        public string Name { get; set; }
        public List<Request> Requests  { get; set; }
    }
}
