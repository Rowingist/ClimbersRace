using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FlyingSampleAnimationController
{
    public static class Params
    {
        public const string DistanceToBalk = "DistanceToBalk";
        public const string Flying = "Flying";
    }

    public static class States
    {
        public const string RightHang = "RightHang";
        public const string LeftHang = "LeftHang";
        public const string MissBalk = "Miss Balk";
        public const string Flying = "Flying";
    }
}