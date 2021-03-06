﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fn = qed.Functions;

namespace qed
{
    public static partial class Handlers
    {
        public static async Task GetBuild(
            IDictionary<string, object> environment,
            dynamic @params,
            Func<IDictionary<string, object>, Task> next)
        {
            var owner = @params.owner as string;
            var name = @params.name as string;
            var id = new Guid(@params.id as string);

            var buildConfiguration = fn.GetBuildConfiguration(owner, name);
            if (buildConfiguration == null)
            {
                environment.SetStatusCode(400);
                return;
            }

            var build = fn.GetBuild(id);
            
            await environment.Render("build", new
            {
                id = build.Id,
                name = build.RepositoryName,
                owner = build.RepositoryOwner,
                output = fn.Redact(build.Ouput)
            });
        }
    }
}
