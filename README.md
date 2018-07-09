ubGridArray
=
A single file that creates an easy to use single array as a 2d array. If you aren't using my [package manager](https://github.com/nhold/ubPackageManager), I recommend putting the `ubGridArray.cs` file under:

`Assets/Plugins/Bifrost/ubGridArray/Scripts/`

Usage
-

First just do the following to get access to the GridArray.

```
using Bifrost.Core;
```

The following example creates a colour based grid array for editing\creating an image. The same can apply for inventory positions or tile map positions.

```
var colorGridArray = new GridArray<Color>(20, 20);

colorGridArray[0, 0] = Color.black;
colorGridArray.SetValueAt(9, 9) = Color.red;

colorGridArray.FloodFill(0, 1, Color.blue, colorGridArray.GetValueAt(0, 1));

```

You can make a GridArray viewable in the inspector like this:

```
public class ColorGridArray : GridArray<Color>
{
}
```

Then you can use it like this in your MonoBehaviour:

```
[SerializeField]
private ColorGridArray colorGridArray; 
```
