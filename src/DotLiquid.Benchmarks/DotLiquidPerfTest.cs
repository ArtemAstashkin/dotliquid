using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace DotLiquid.Benchmarks
{
    public class DotLiquidPerfTest
    {
        private Template CompiledTemplate { get; }
        private List<string> TemplateTokens { get; }

        public DotLiquidPerfTest()
        {
            Template.NamingConvention = new IgnoreCaseNamingConvention();
            TemplateTokens = Template.GetReusableTokens(Resource.Template);
            CompiledTemplate = Template.Parse(Resource.Template);
        }

        [Benchmark]
        public string FromTemplateWithInt()
        {
            var random = new Random();
            var model = GetModel(random.Next());

            return RenderTemplate(model, CompiledTemplate);
        }

        [Benchmark]
        public string FromTemplateWithLong()
        {
            var random = new Random();
            var model = GetModel(long.MaxValue - random.Next(1000));

            return RenderTemplate(model, CompiledTemplate);
        }

        [Benchmark]
        public string FromTokensWithLong()
        {
            var random = new Random();
            var model = GetModel(long.MaxValue - random.Next(1000));

            return RenderTemplate(model, TemplateTokens);
        }

        private string RenderTemplate(dynamic model, List<string> tokens)
        {
            var copiedTokens = new List<string>(tokens);

            var compiledTemplate = Template.ParseTokens(copiedTokens);
            var dataHash = Hash.FromAnonymousObject(model);
            return compiledTemplate.Render(
                new RenderParameters { LocalVariables = dataHash, RethrowErrors = true });
        }

        private string RenderTemplate(dynamic model, Template compiledTemplate)
        {
            var dataHash = Hash.FromAnonymousObject(model);
            return compiledTemplate.Render(
                new RenderParameters { LocalVariables = dataHash, RethrowErrors = true });
        }

        private dynamic GetModel(dynamic id)
        {
            var random = new Random();

            var popular = new List<dynamic>();
            for (var i = 0; i < random.Next(50); i++)
                popular.Add(
                    new
                    {
                        Vendor = random.NextDouble().ToString(),
                        TypePrefix = random.NextDouble().ToString(),
                        OldPrice = random.Next(),
                        Price = random.NextDouble(),
                        Id = id
                    });

            return new { VisitorCategoryInterest = new { Popular = popular } };
        }
    }
}
