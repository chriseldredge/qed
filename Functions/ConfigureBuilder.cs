﻿namespace qed
{
    public static partial class Functions
    {
        public static void ConfigureBuilder(OwinBuilder builder)
        {
            builder.Use(Mustache.Create(
                templateRootDirectoryName: "MustacheTemplates",
                layoutTemplateName: "_layout"));

            builder.Use(Dispatcher.Create(dispatcher =>
            {
                dispatcher.Get("/", Handlers.GetHome);
                dispatcher.Post("/events/push", Handlers.PostPushEvent);
                dispatcher.Get("/{owner}/{name}", Handlers.GetBuilds);
                dispatcher.Get("/{owner}/{name}/builds/{id}", Handlers.GetBuild);
            }));
        }
    }
}
