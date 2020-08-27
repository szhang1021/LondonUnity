using System.Collections.Generic;
using Google.Maps.Event;
using Google.Maps.Feature.Style;
using Google.Maps.Unity.Intersections;
using UnityEngine;

namespace Google.Maps.Examples {
  /// <summary>
  /// Applies materials to intersections and removes 3-way intersections.
  /// </summary>
  [RequireComponent(typeof(MapsService))]
  public class Intersections : MonoBehaviour {
    /// <summary>
    /// The default material to apply to intersections.
    /// </summary>
    public Material DefaultMaterial;

    /// <summary>
    /// The possible materials to apply to intersection arms.
    /// </summary>
    public Material[] ArmMaterials;

    /// <summary>
    /// The width of the road segments.
    /// </summary>
    public float RoadWidth = 3;

    void Awake() {
      // Subscribe to intersection and segment events.
      MapsService mapsService = GetComponent<MapsService>();
      mapsService.Events.IntersectionEvents.WillCreate.AddListener(OnWillCreateIntersection);
      mapsService.Events.SegmentEvents.WillCreate.AddListener(OnWillCreateSegment);
    }

    private void OnWillCreateSegment(WillCreateSegmentArgs args) {
      // Set the width of the road segments.
      args.Style = new SegmentStyle.Builder(args.Style) {
            Width = RoadWidth
        }.Build();
    }

    void OnWillCreateIntersection(WillCreateIntersectionArgs args) {
      // Set reasonable default settings for intersections.
      args.Style = new SegmentStyle.Builder(args.Style) {
          IntersectionMaterial = DefaultMaterial,
          IntersectionArmLength = RoadWidth,
          IntersectionJoinLength = RoadWidth * 4,
          MaxIntersectionArmDistance = RoadWidth * 5,
          Width = RoadWidth
      }.Build();

      // Get the intersection arms from the feature shape.
      List<IntersectionArm> arms = new List<IntersectionArm>(args.MapFeature.Shape.Arms);

      if (arms.Count != 3) {
        // Not a 3-way intersection. Apply some random arm materials and return.
        foreach (IntersectionArm arm in arms) {
          // Don't change the material of two-way arms. These arms are treated as part of the inner
          // polygon (i.e. the center of the intersection where roads overlap) and should use the
          // default intersection material.
          if (arm.IsTwoWay(args.Style.IntersectionJoinLength)) {
            continue;
          }

          arm.Material = ArmMaterials[Random.Range(0, ArmMaterials.Length)];
        }
      } else {
        // Delete 3-way intersections.
        args.Cancel = true;
      }
    }
  }
}
