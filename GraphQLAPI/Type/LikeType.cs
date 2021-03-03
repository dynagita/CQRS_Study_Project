using GraphQL.Types;
using GraphQLAPI.Domain;

namespace GrapgQLAPI.Types
{
    public class LikeType : ObjectGraphType<Like>
    {
        public LikeType()
        {
            Field(x => x.Id, type: typeof(IdGraphType))
                .Name("hash")
                .Description("Like's hash.");

            Field(x => x.WritableRelation)
                .Name("id")
                .Description("Like id");

            Field(x => x.UserId)
                .Name("userId")
                .Description("Like UserId");

            Field(x => x.ArticleId)
                .Name("articleId")
                .Description("Like ArticleId");
        }
    }
}
