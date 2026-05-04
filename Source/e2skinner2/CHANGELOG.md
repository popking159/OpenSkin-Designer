# Changelog

## 4.2.0.0 - Gradient backgroundColor and cornerRadius mask support

### Added
- Added support for Enigma2-style `backgroundColor` gradients on `eLabel`.
- Added support for 3-part gradient syntax:
  - `backgroundColor="#00101010,#00303030,vertical"`
  - `backgroundColor="red,green,vertical"`
  - `backgroundColor="#00ff0000,#0000ff00,vertical"`
- Added support for preserving extended `cornerRadius` syntax:
  - `cornerRadius="30;topLeft,topRight"`
  - `cornerRadius="30;bottomLeft,bottomRight"`
  - `cornerRadius="30;left"`
  - `cornerRadius="30;right"`

### Changed
- Improved color parsing so comma-separated gradient values are no longer treated as invalid single colors.
- Updated `eLabel` background handling so `backgroundColor` can be used for both normal colors and gradients.
- Updated `cornerRadius` handling to save the original raw value instead of reducing it to a plain numeric radius.
- Improved preview rendering so corner-radius edge masks are respected visually.

### Fixed
- Fixed saving of `backgroundColor="#00101010,#00303030,vertical"`.
- Fixed saving of `backgroundColor="red,green,vertical"`.
- Fixed saving of `backgroundColor="#00ff0000,#0000ff00,vertical"`.
- Fixed saving of `cornerRadius="30;topLeft,topRight"`.
- Fixed preview issue where `cornerRadius="30;topLeft,topRight"` was displayed as rounded on all four corners.

### Notes
- Existing normal color values such as `backgroundColor="#00ffffff"` and `backgroundColor="red"` continue to work.
- Existing simple radius values such as `cornerRadius="30"` continue to round all corners.