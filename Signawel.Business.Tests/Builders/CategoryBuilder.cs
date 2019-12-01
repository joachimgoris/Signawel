using Signawel.Domain;
using System;

namespace Signawel.Business.Builders
{
    public class CategoryBuilder
    {
        private readonly Category _category;
        private readonly Random _random;

        public CategoryBuilder()
        {
            _random = new Random();
            _category = new Category()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
                OrderId = _random.Next(0, 100),
                ImagePath = Guid.NewGuid().ToString()
            };
        }

        public CategoryBuilder WithOrderId(int id)
        {
            _category.OrderId = id;
            return this;
        }

        public Category Build()
        {
            return _category;
        }
    }
}
