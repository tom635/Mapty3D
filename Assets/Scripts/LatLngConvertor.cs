using System;
using Mapzen;
using UnityEngine;
using System.Collections.Generic;

public class LatLngConvertor
{


    // Semi-axes of WGS-84 geoidal reference
    private const double WGS84_a = 6378137.0; // Major semiaxis [m]
    private const double WGS84_b = 6356752.3; // Minor semiaxis [m]

    // 'halfSideInKm' is the half length of the bounding box you want in kilometers.
    public static TileArea GetBoundingBox(List<string> boundingBox, double halfSideInKm, int ZoomLevel)
    {
        Debug.Log("Hehe " + boundingBox[0] + "-" + boundingBox[1]);
        //MapPoint point = new MapPoint(Convert.ToDouble(boundingBox[0]), Convert.ToDouble(boundingBox[1]));
        // Bounding box surrounding the point at given coordinates,
        // assuming local approximation of Earth surface as a sphere
        // of radius given by WGS84
        var lat = Deg2rad(Convert.ToDouble(boundingBox[1]));
        var lon = Deg2rad(Convert.ToDouble(boundingBox[0]));
        var halfSide = 1000 * halfSideInKm;
        // Radius of Earth at given latitude
        var radius = WGS84EarthRadius(lat);
        // Radius of the parallel at given latitude
        var pradius = radius * Math.Cos(lat);
        var latMin = Rad2deg(lat - halfSide / radius);
        var latMax = Rad2deg(lat + halfSide / radius);
        var lonMin = Rad2deg(lon - halfSide / pradius);
        var lonMax = Rad2deg(lon + halfSide / pradius);
        Debug.Log(lonMin + "-" + latMin);
        Debug.Log(lonMax + "-" + latMax);
        TileArea box = new TileArea(
                new LngLat(lonMin, latMin),
                new LngLat(lonMax, latMax),
                ZoomLevel);
        return box;
    }

    // degrees to radians
    private static double Deg2rad(double degrees)
    {
        return Math.PI * degrees / 180.0;
    }

    // radians to degrees
    private static double Rad2deg(double radians)
    {
        return 180.0 * radians / Math.PI;
    }

    // Earth radius at a given latitude, according to the WGS-84 ellipsoid [m]
    private static double WGS84EarthRadius(double lat)
    {
        // http://en.wikipedia.org/wiki/Earth_radius
        var An = WGS84_a * WGS84_a * Math.Cos(lat);
        var Bn = WGS84_b * WGS84_b * Math.Sin(lat);
        var Ad = WGS84_a * Math.Cos(lat);
        var Bd = WGS84_b * Math.Sin(lat);
        return Math.Sqrt((An * An + Bn * Bn) / (Ad * Ad + Bd * Bd));
    }
}

public class MapPoint
{
    public double Longitude { get; set; } // In Degrees
    public double Latitude { get; set; } // In Degrees
    public MapPoint(double lon, double lat)
    {
        this.Longitude = lon;
        this.Latitude = lat;
    }
}