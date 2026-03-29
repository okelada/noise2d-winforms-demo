using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noise2D
{
    internal class SeamlessOverlap
    {
        int width;
        int height;
        private int _overlap;

        public SeamlessOverlap(int width, int height, int overlap)
        {
            this.width = width;
            this.height = height;
            this._overlap = overlap;
        }

        public float[] GetSeamlessBufferUpper(float[] baseBuffer, out int outImageWidth, out int outImageHeight)
        {
            int overlap = _overlap;

            outImageWidth = (width / 2) * 2;
            outImageHeight = (height / 2) * 2;

            outImageWidth -= 2 * overlap;
            if (overlap > outImageWidth / 2)
            {
                outImageWidth = width / 2;
                overlap = outImageWidth / 2;
            }

            float[] seamlessBuffer = new float[outImageWidth * outImageHeight];

            for (int i = 0; i < seamlessBuffer.Length; i++)
                seamlessBuffer[i] = float.NaN;

            int cj = outImageHeight / 2;
            int ci = outImageWidth / 2;

            int offsetJ = -height / 2;
            int offsetI = -width / 2;

            ////4th quadrant
            for (int j = height / 2; j < height; ++j)
            {
                for (int i = width / 2; i < width; ++i)
                {
                    int pos = (j + offsetJ) * outImageWidth + (i + offsetI);
                    seamlessBuffer[pos] = baseBuffer[j * width + i];
                }
            }

            offsetI = Math.Max(0, ci - overlap);

            //3rd quadrant
            for (int j = height / 2; j < height; ++j)
            {
                for (int i = 0; i < width / 2; ++i)
                {
                    int pos = (j + offsetJ) * outImageWidth + (i + offsetI);

                    if (float.IsNaN(seamlessBuffer[pos]))
                        seamlessBuffer[pos] = baseBuffer[j * width + i];
                    else
                    {
                        float blendFactor = 0.0f;
                        int outputCol = i + offsetI;


                        if (outputCol >= (ci - overlap) && outputCol <= (ci + overlap))
                            blendFactor = 1.0f - (float)(outputCol - (ci - overlap)) / (2.0f * overlap);

                        seamlessBuffer[pos] = seamlessBuffer[pos] * (blendFactor) + baseBuffer[j * width + i] * (1.0f - blendFactor);
                    }
                }
            }

            return seamlessBuffer;
        }

        public float[] GetSeamlessBufferLower(float[] baseBuffer, out int outImageWidth, out int outImageHeight)
        {
            int overlap = _overlap;

            outImageWidth = (width / 2) * 2;
            outImageHeight = (height / 2) * 2;

            outImageWidth -= 2 * overlap;
            if (overlap > outImageWidth / 2)
            {
                outImageWidth = width / 2;
                overlap = outImageWidth / 2;
            }

            float[] seamlessBuffer = new float[outImageWidth * outImageHeight];

            for (int i = 0; i < seamlessBuffer.Length; i++)
                seamlessBuffer[i] = float.NaN;

            int cj = outImageHeight / 2;
            int ci = outImageWidth / 2;

            int offsetJ = Math.Max(0, cj);
            int offsetI = Math.Max(0, ci - overlap);

            //1st quadrant
            for (int j = 0; j < height / 2; ++j)
            {
                for (int i = 0; i < width / 2; ++i)
                {
                    int pos = (j + offsetJ) * outImageWidth + (i + offsetI);
                    seamlessBuffer[pos] = baseBuffer[j * width + i];
                }
            }

            //////2nd quadrant
            for (int j = 0; j < height / 2; ++j)
            {
                for (int i = width / 2; i < width; ++i)
                {
                    int pos = (j + offsetJ) * outImageWidth + (i - width / 2);

                    if (float.IsNaN(seamlessBuffer[pos]))
                        seamlessBuffer[pos] = baseBuffer[j * width + i];
                    else
                    {
                        float blendFactor = 0.0f;
                        int outputCol = i - width / 2;

                        if (outputCol > (ci - overlap) && outputCol <= (ci + overlap))
                            blendFactor = (float)(outputCol - (ci - overlap)) / (2.0f * overlap);

                        seamlessBuffer[pos] = seamlessBuffer[pos] * (blendFactor) + baseBuffer[j * width + i] * (1.0f - blendFactor);
                    }
                }
            }

            return seamlessBuffer;

        }


        public float[] GetSeamlessBuffer(float[] baseBuffer, out int outImageWidth, out int outImageHeight)
        {
            int overlap = _overlap;

            float[] seamlessBufferLower = GetSeamlessBufferLower(baseBuffer, out int outImageWidthL, out int outImageHeightL);
            float[] seamlessBufferUpper = GetSeamlessBufferUpper(baseBuffer, out int outImageWidthU, out int outImageHeightU);

            outImageWidth = outImageWidthL;

            outImageHeight = (outImageHeightL / 2) * 2;
            outImageHeight -= 2 * overlap;

            if (overlap > outImageHeight / 2)
            {
                outImageHeight = outImageHeightL / 2;
                overlap = outImageHeight / 2;
            }

            float[] seamlessBuffer = new float[outImageWidth * outImageHeight];

            for (int i = 0; i < seamlessBuffer.Length; i++)
                seamlessBuffer[i] = float.NaN;

            int cj = outImageHeight / 2;

            //upper hemisphere
            for (int j = 0; j < outImageHeightU / 2; ++j)
            {
                for (int i = 0; i < outImageWidthU; ++i)
                {
                    int pos = j * outImageWidth + i;
                    seamlessBuffer[pos] = seamlessBufferUpper[j * outImageWidthU + i];
                }
            }

            int offsetJ = Math.Min(outImageHeightL / 2, 2 * overlap);

            ////lower hemisphere
            for (int j = outImageHeightL / 2; j < outImageHeightL; ++j)
            {
                for (int i = 0; i < outImageWidthL; ++i)
                {
                    int pos = (j - offsetJ) * outImageWidth + i;
                    if (float.IsNaN(seamlessBuffer[pos]))
                        seamlessBuffer[pos] = seamlessBufferLower[j * outImageWidthL + i];
                    else
                    {
                        float blendFactor = 0.0f;
                        int outputRow = j - offsetJ;


                        if (outputRow >= (cj - overlap) && outputRow < (cj + overlap))
                            blendFactor = 1.0f - (float)(outputRow - (cj - overlap)) / (2.0f * overlap);

                        seamlessBuffer[pos] = seamlessBuffer[pos] * (blendFactor) + seamlessBufferLower[j * outImageWidthL + i] * (1.0f - blendFactor);
                    }
                }
            }

            return seamlessBuffer;
        }
    }
}
