using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Direction
{
    [Test]
    public void Right()
    {
        
        Assert.AreEqual(new Vector2(1, 0), Vector2.right);
    }

    [Test]
    public void Left()
    {
        Assert.AreEqual(new Vector2(-1, 0), Vector2.left);
    }
}
