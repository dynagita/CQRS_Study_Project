using GraphQL.Types;
using GraphQLAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLAPI.Type
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            Field(x => x.Id, type: typeof(IdGraphType))
                .Name("hash")
                .Description("User's hash.");

            Field(x => x.WritableRelation)
                .Name("id")
                .Description("User's id.");

            Field(x => x.Name)
                .Name("name")
                .Description("User's name.");

            Field(x => x.LastName)
                .Name("lastName")
                .Description("User's last name.");


            Field(x => x.Email)
                .Name("email")
                .Description("User's e-mail.");

        }
    }
}
