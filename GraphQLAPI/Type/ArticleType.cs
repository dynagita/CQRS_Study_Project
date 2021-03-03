using GraphQL.Types;
using GraphQLAPI.Domain;

namespace GrapgQLAPI.Type
{
    public class ArticleType : ObjectGraphType<Article>
    {
        public ArticleType()
        {
            Field(x => x.Abstract)
                .Name("abstract")
                .Description("Article abstract");

            Field(x => x.Author.Name)
                .Name("authorName")
                .Description("Article author");
            Field(x => x.Author.LastName)
                .Name("authorLastName")
                .Description("Article author");
            Field(x => x.Author.Email)
                .Name("authorEmail")
                .Description("Article author");

            Field(x => x.Content)
                .Name("content")
                .Description("Article content");

            Field(x => x.Id, type: typeof(IdGraphType))
                .Name("hash")
                .Description("Article hash");

            Field(x => x.WritableRelation)
                .Name("id")
                .Description("Article id");

            Field(x => x.Subject)
                .Name("subject")
                .Description("Article subject");

            Field(x => x.TotalLike)
                .Name("totalLike")
                .Description("Article TotalLike");

        }
    }
}
