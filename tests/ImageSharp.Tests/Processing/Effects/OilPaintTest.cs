// <copyright file="OilPaintTest.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp.Tests
{
    using ImageSharp.PixelFormats;

    using Xunit;

    public class OilPaintTest : FileTestBase
    {
        public static readonly TheoryData<int, int> OilPaintValues
            = new TheoryData<int, int>
            {
                { 15, 10 },
                { 6, 5 }
            };

        [Theory]
        [WithFileCollection(nameof(DefaultFiles), nameof(OilPaintValues), DefaultPixelType)]
        public void ImageShouldApplyOilPaintFilter<TPixel>(TestImageProvider<TPixel> provider, int levels, int brushSize)
            where TPixel : struct, IPixel<TPixel>
        {
            using (Image<TPixel> image = provider.GetImage())
            {
                image.OilPaint(levels, brushSize)
                    .DebugSave(provider, string.Join("-", levels, brushSize), Extensions.Bmp);
            }
        }

        [Theory]
        [WithFileCollection(nameof(DefaultFiles), nameof(OilPaintValues), DefaultPixelType)]
        public void ImageShouldApplyOilPaintFilterInBox<TPixel>(TestImageProvider<TPixel> provider, int levels, int brushSize)
            where TPixel : struct, IPixel<TPixel>
        {
            using (Image<TPixel> source = provider.GetImage())
            using (var image = new Image<TPixel>(source))
            {
                var bounds = new Rectangle(10, 10, image.Width / 2, image.Height / 2);

                image.OilPaint(levels, brushSize, bounds)
                    .DebugSave(provider, string.Join("-", levels, brushSize), Extensions.Bmp);

                ImageComparer.EnsureProcessorChangesAreConstrained(source, image, bounds, 0.001F);
            }
        }
    }
}