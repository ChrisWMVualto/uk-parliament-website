using System;

namespace UKP.Website.Service.Model
{
    public class StackModel
    {
        public StackModel(Guid id, string description, string iasDisplayAs, int? sortOrder)
        {
            Id = id;
            Description = description;
            IasDisplayAs = iasDisplayAs;
            SortOrder = sortOrder;
        }

        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public string IasDisplayAs { get; private set; }
        public int? SortOrder { get; private set; }
    }
}