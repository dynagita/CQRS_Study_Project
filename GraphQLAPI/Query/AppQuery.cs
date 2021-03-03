using GrapgQLAPI.Type;
using GraphQL.Types;
using GraphQLAPI.Domain;
using GraphQLAPI.Repository.Interface;
using GraphQLAPI.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLAPI.Query
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(IUserRepository userRepo, IArticleRepository articleRepo)
        {
            Field<ListGraphType<UserType>>("users", arguments: new QueryArguments(new List<QueryArgument>()
            {
                new QueryArgument<IdGraphType>
                {
                    Name = "id"
                },
                new QueryArgument<IdGraphType>
                {
                    Name = "name"
                },
                new QueryArgument<IdGraphType>
                {
                    Name = "lastName"
                },
                new QueryArgument<IdGraphType>
                {
                    Name = "email"
                }
            })
            , resolve: (Func<ResolveFieldContext<object>, object>)(context =>
            {
                
                var user = GetUser(context);                
                return (object)userRepo.ListFilteringByEntity(user);

            }));

            Field<UserType>("user", arguments: new QueryArguments(new List<QueryArgument>()
            {
                new QueryArgument<IdGraphType>
                {
                    Name = "id"
                },
                new QueryArgument<IdGraphType>
                {
                    Name = "name"
                },
                new QueryArgument<IdGraphType>
                {
                    Name = "lastName"
                },
                new QueryArgument<IdGraphType>
                {
                    Name = "email"
                }
            })
            , resolve: (Func<ResolveFieldContext<object>, object>)(context =>
            {
                var user = GetUser(context);
                
                return (object)userRepo.GetFilteringByEntity(user);
            }));

            Field<ListGraphType<ArticleType>>("articles", arguments: new QueryArguments(new List<QueryArgument>()
            {
                new QueryArgument<IdGraphType>
                {
                    Name = "abstract"
                },
                new QueryArgument<IdGraphType>
                {
                    Name = "author"
                },
                new QueryArgument<IdGraphType>
                {
                    Name = "subject"
                },
                new QueryArgument<IdGraphType>
                {
                    Name = "authorName"
                },
                new QueryArgument<IdGraphType>
                {
                    Name = "abstract"
                },
                new QueryArgument<IdGraphType>
                {
                    Name = "authorLastName"
                },
                new QueryArgument<IdGraphType>
                {
                    Name = "authorEmail"
                }

            })
            , resolve: context =>
            {

                var Article = GetArticle(context);
                return articleRepo.ListFilteringByEntity(Article);

            });

            Field<ArticleType>("article", arguments: new QueryArguments(new List<QueryArgument>()
            {
                new QueryArgument<IdGraphType>
                {
                    Name = "id"
                },
                new QueryArgument<IdGraphType>
                {
                    Name = "abstract"
                },
                new QueryArgument<IdGraphType>
                {
                    Name = "author"
                },
                new QueryArgument<IdGraphType>
                {
                    Name = "authorName"
                },
                new QueryArgument<IdGraphType>
                {
                    Name = "abstract"
                },
                new QueryArgument<IdGraphType>
                {
                    Name = "authorLastName"
                },
                new QueryArgument<IdGraphType>
                {
                    Name = "authorEmail"
                }
            })
            , resolve: context =>
            {
                var Article = GetArticle(context);

                return articleRepo.GetFilteringByEntity(Article);
            });
        }

        private User GetUser(ResolveFieldContext<object> context)
        {
            var id = context.GetArgument<string>("id");
            var name = context.GetArgument<string>("name");
            var lastName = context.GetArgument<string>("lastName");
            var email = context.GetArgument<string>("email");

            return new User()
            {
                Id = id,
                Name = name,
                LastName = lastName,
                Email = email
            };
        }

        private Article GetArticle(ResolveFieldContext<object> context)
        {
            var id = context.GetArgument<string>("id");
            var abstract_ = context.GetArgument<string>("abstract");
            var author = context.GetArgument<string>("author");
            var subject = context.GetArgument<string>("subject");
            var email = context.GetArgument<string>("authorEmail");
            var lastName = context.GetArgument<string>("authorLastName");
            var name = context.GetArgument<string>("authorName");

            return new Article()
            {
                Id = id,
                Abstract = abstract_,
                Author = new User()
                {
                    Name = name,
                    LastName = lastName,
                    Email = email
                },
                Subject = subject
            };
        }
    }
}
