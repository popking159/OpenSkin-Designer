# Changelog

## v4.2.1.0 MOD by odem2014

### Added
- Added support for Enigma2-style `backgroundColor` gradients on `eLabel` and label-style widgets.
- Added support for 3-part and 4-part gradient syntax:
  - `backgroundColor="#00101010,#00303030,vertical"`
  - `backgroundColor="red,green,vertical"`
  - `backgroundColor="#00101010,#00202020,#00303030,vertical"`
- Added support for preserving extended `cornerRadius` syntax:
  - `cornerRadius="30;topLeft,topRight"`
  - `cornerRadius="30;bottomLeft,bottomRight"`
  - `cornerRadius="30;left"`
  - `cornerRadius="30;right"`
- Added property-grid controls for corner radius size and corner direction presets.
- Added property-grid controls for gradient start, middle, end, and direction values.
- Added progress widget controls for separate background and foreground colors.
- Added progress foreground gradient controls: start color, middle color, end color, and direction.
- Added listbox controls for normal and selected item gradients, item corner radius, selected item corner radius, selection index, selection zoom, list orientation, item spacing, and move colors.
- Added scrollbar preview support for width, offset, radius, foreground color, background color, border color, and border width.
- Added Arabic language file support in the language folder.

### Changed
- Updated app title/product name to `OpenSkin Designer MOD by odem2014`.
- Updated assembly version and file version to `4.2.1.0`.
- Improved property-grid refresh behavior so new custom fields update XML, editor text, and preview immediately.
- Improved build output behavior so runtime folders such as `skins`, `elements`, `languages`, and `fonts` are copied into `bin` output folders.
- Improved listbox preview for vertical and grid layouts.
- Improved scrollbar preview rendering and clipping behavior.

### Fixed
- Fixed saving of comma-separated gradient values that start with `#`.
- Fixed color parsing so gradient values are not treated as invalid single hex colors.
- Fixed saving of `cornerRadius="30;topLeft,topRight"` without reducing it to only `30`.
- Fixed preview rendering so corner-radius masks only round the selected corners.
- Fixed progress preview for gradient `foregroundColor` values such as `green,yellow,red,horizontal`.
- Fixed listbox selected item preview for `itemGradientSelected` and `itemCornerRadiusSelected`.
- Fixed listbox grid preview for `listOrientation="grid"`, `itemSpacing`, and `selectionZoom`.
- Fixed scrollbar radius preview for `scrollbarRadius`.
- Fixed null-gradient crashes in listbox preview.
- Fixed EventTime converter preview crashes when preview source data is missing.
- Cleaned bundled ScintillaNET build warnings without changing runtime behavior.

### Notes
- Existing normal color values such as `backgroundColor="#00ffffff"` and `backgroundColor="red"` continue to work.
- Existing simple radius values such as `cornerRadius="30"` continue to round all corners.
- Keep the project target as `x86` when using the bundled old ScintillaNET/SciLexer components.
