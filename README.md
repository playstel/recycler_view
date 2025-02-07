# recycler_view
The best practice for rendering lists and tables in Unity UI: recycling elements using object pooling and dynamic loading (virtualization)

The implementation is based on ScrollRect + Mask + Content Size Fitter, as well as creating a limited number of UI elements that are reused when scrolling.

# How does it work?
1) A pool of a limited number of elements is created (e.g. those displayed on the screen + a small reserve).
2) UI objects are reused instead of being created/destroyed on scroll.
3) Only visible elements are rendered, the rest are hidden or moved.
4) The position of elements is updated, and their data is replaced by the list index.

# Performance optimization (no constant creation/deletion of objects).
✅ Memory and CPU/GPU saving (only the required number of elements is rendered).
✅ Smooth scrolling without freezes (especially on large lists of 1000+ elements).

This approach is used in UINavigationController (iOS), RecyclerView (Android) and similar solutions.

⚡ Applicable to: inventories, leaderboards, shops, player lists in multiplayer, etc.
