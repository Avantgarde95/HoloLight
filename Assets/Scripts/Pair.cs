using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;

class Pair
{
    private static System.Random random = new System.Random();
    private static int windowRadius = 3;
    private static int windowSize = windowRadius * 2 + 1;

    private GameObject imageObject;
    private Renderer imageRenderer;
    private VideoPlayer imageVideoPlayer;

    private GameObject modelObject;
    private Renderer modelRenderer;

    private Texture2D tempTexture = null;

    private int rotationDirection = (random.Next(0, 2) == 0) ? 1 : -1;
    private float rotationSpeed = ((float)random.NextDouble()) + 0.3f;

    public Pair(string name)
    {
        imageObject = GameObject.Find(name + "/" + "Image");
        imageRenderer = imageObject.GetComponent<Renderer>();
        imageVideoPlayer = imageObject.GetComponent<VideoPlayer>();

        modelObject = GameObject.Find(name + "/" + "Model");
        modelRenderer = modelObject.GetComponent<Renderer>();
    }

    public void animateModel()
    {
        modelObject.transform.Rotate(rotationDirection * rotationSpeed * Vector3.up, Space.World);
    }

    public void mapVideoToImage()
    {
        if (imageVideoPlayer.texture == null)
        {
            return;
        }

        imageRenderer.material.SetTexture("_MainTex", imageVideoPlayer.texture);
    }

    public void mapVideoToModel()
    {
        if (imageVideoPlayer.texture == null)
        {
            return;
        }

        var renderTexture = imageVideoPlayer.texture as RenderTexture;
        var width = renderTexture.width;
        var height = renderTexture.height;
        RenderTexture.active = renderTexture;

        if (tempTexture == null)
        {
            tempTexture = new Texture2D(width, height, TextureFormat.RGB24, false);
        }

        tempTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        var colors = tempTexture.GetPixels(width / 2 - windowRadius, height / 2 - windowRadius, windowSize, windowSize).ToList();
        var averageColor = colors.Aggregate(new Color(0, 0, 0), (result, color) => result + color / (windowSize * windowSize));
        modelRenderer.material.color = averageColor;
    }
}
