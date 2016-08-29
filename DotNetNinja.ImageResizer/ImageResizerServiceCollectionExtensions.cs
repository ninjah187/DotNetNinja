using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetNinja.ImageResizer
{
    public static class ImageResizerServiceCollectionExtensions
    {
        public static IServiceCollection AddImageResizer(this IServiceCollection services)
        {
            services
                .AddSingleton<IImagePathGenerator, ImagePathGenerator>()
                .AddSingleton<IImageResizer, ImageResizer>(
                    serviceProvider => (ImageResizer) new ImageResizerBuilder()
                                                        .AllowAll()
                                                        .Build()
                    );

            return services;
        }

        public static IServiceCollection AddImageResizer<TImagePathGenerator, TImageResizer>(this IServiceCollection services)
            where TImagePathGenerator : class, IImagePathGenerator
            where TImageResizer : class, IImageResizer
        {
            services
                .AddSingleton<IImagePathGenerator, TImagePathGenerator>()
                .AddSingleton<IImageResizer, TImageResizer>();

            return services;
        }
    }
}
