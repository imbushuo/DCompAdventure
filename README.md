# HWND coexistence in WinUI 3 XAML with undocumented Microsoft.UI.Composition interfaces

![image](https://user-images.githubusercontent.com/6284353/170027620-fab690b5-9cd2-485d-a716-56f3b0bfb9b8.png)

This is an attempt of the POC per [this discussion thread](https://github.com/microsoft/microsoft-ui-xaml/issues/1833) and [this issue](https://github.com/microsoft/microsoft-ui-xaml/issues/3859) using undocumented Microsoft.UI.Composition interfaces.

# Known Limitation

- Need to manually handle input hit testing, DPI changes and other input and windowing primitives.
- Need to manually handle D2D and DComp device lost events.
- Certain XAML control may have rendering issues.
- The provided code might have some reference count leak that I haven't got time to address. After all it's a POC.

# License

MIT License

# Acknowledgements

- [ADeltax](https://blog.adeltax.com/interopcompositor-and-coredispatcher/)
- 
