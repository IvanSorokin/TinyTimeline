using DataAccess.Documents;
using DataAccess.Interfaces.Mappers;
using Domain.Objects;

namespace DataAccess.Concrete.Mappers
{
    public class ReviewMapper : ITwoWayMapper<ReviewDocument, Review>
    {
        public ReviewDocument Map(Review obj) => new ReviewDocument
        {
            Content = obj.Content,
            Rating = obj.Rating,
            Id = obj.Id
        };


        public Review Map(ReviewDocument obj) => new Review
        {
            Content = obj.Content,
            Rating = obj.Rating,
            Id = obj.Id
        };
    }
}