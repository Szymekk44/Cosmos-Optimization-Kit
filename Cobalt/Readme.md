<p align="center">
 <picture>
    <source srcset="../Artwork/Cobalt.png">
    <img width=65%>
  </picture>
</p><br>

# How does it work?
## Canvas Caching
As you probably already know DrawImageAlpha is quite <b>VERY SLOW</b>.<br>
Unfortunately, you still have to use it to draw mouse cursor, TTF text or simply transparent images.
<b>Thanks to caching, we can first render something using DrawImageAlpha, save it to a bitmap along with the background and draw it like a regular image!</b>
Thanks to this, you can also dynamically change wallpaper size without losing 80% fps!

## TTF Pre-Renderer
If we want to change some text, e.g. the number of fps or input in the terminal, we cannot cache the entire text because it will become <b>static</b>.<br>
How can we fix it?<br>
We can cache every TTF character!
This is probably the best method for drawing text.
Note that this is based on canvas caching, its really powerful!
> [!NOTE]
> Remember that the text will be drawn WITH the solid color background (Remember that you can create cache with multiple backgrounds!), if you want to draw it on images like wallpaper, it won't work.<br>
> You can still use first method.

## Heap.Collect()
Instead of calling Heap.Collect() every frame, we can do it every 4th frame. Thanks to this, our FPS will increase and larger lag spikes won't appear.
You can check many settings, but we do not recommend going above 20. Then there is a lot of fps, but it stops being smooth because Heap.Collect starts to take a lot of time.
> [!IMPORTANT]
> Completely removing Heap.Collect() may cause a temporary increase in FPS, but your system will crash after a short while.
