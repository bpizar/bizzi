using JayGor.People.Entities.Entities;
using System;
using JayGor.People.Entities.Requests;

namespace JayGor.People.Entities.Responses
{
    public class SaveTaskRequest : CommonRequest
    {
        public tasks Task { get; set; }
        public duplicate_tasks DuplicateTask { get; set; }
        public Boolean EditingSerie { get; set; }
    }
}