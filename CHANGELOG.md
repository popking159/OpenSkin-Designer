# Changelog

### v4.2.2.0 MOD by odem2014
created by [odem2014](https://github.com/odem2014)
* Updated application title/version to `OpenSkin Designer MOD by odem2014 v4.2.2.0`.
* Added designer-tree Move Up and Move Down controls for screens, panels, widgets, labels, pixmaps, and other items.
* Added right-click Copy, Cut, and Paste support in the designer tree so items can be copied or moved between screens without using the code editor.
* Fixed paste behavior so screens/items are inserted below the highlighted screen/item instead of always at the end.
* Fixed designer-tree move/copy/cut/paste to work with screens and panels loaded from included XML files such as `skin_templates.xml`.
* Fixed code-panel editing and saving for screens/panels inside included XML files.
* Fixed Cut/Paste ghost-item issue where the XML was moved but the old item remained visible in the left tree.
* Fixed repeated unsaved-changes prompts after moving or pasting items; the warning now appears only when closing/opening/reloading before saving.
* Added Ctrl+click multi-select support in the designer preview panel.
* Added visible multi-selection outlines in the preview panel.
* Added group dragging so multiple selected items within the same screen can be moved together.
* Fixed designer preview crashes caused by invalid or unresolved pixmap/widget image sizes.

### v4.2.1.0 MOD by odem2014
created by [odem2014](https://github.com/odem2014)
* Updated application title/version to `OpenSkin Designer MOD by odem2014 v4.2.1.0`.
* Added property-grid controls for corner radius size and corner direction presets.
* Added property-grid controls for label gradient start/middle/end colors and gradient direction.
* Added progress widget background and foreground color controls, including foreground gradient start/middle/end/direction fields.
* Added support for slider-style widgets without `render="Progress"` to preview and edit like progress bars.
* Improved Listbox property-grid control for normal/selected item gradients, item corner radius, selected item radius, selection index, wrap-around, and foreground/background colors.
* Improved Listbox grid preview support for `listOrientation="grid"`, `itemWidth`, `itemHeight`, `itemSpacing`, `selectionZoom`, `moveBackgroundColor`, and `moveFontColor`.
* Fixed Listbox scrollbar preview for `scrollbarWidth`, `scrollbarOffset`, `scrollbarRadius`, `scrollbarForegroundColor`, `scrollbarBackgroundColor`, `scrollbarBorderColor`, and `scrollbarBorderWidth`.
* Fixed null-gradient crashes in Listbox preview by safely falling back when a gradient is incomplete.
* Fixed invalid pixmap/widget preview image sizes so the designer no longer crashes while resizing bad or unresolved preview images.
* Added build copy support so runtime folders such as `skins`, `elements`, `languages`, and `fonts` are copied to the output folder.

### v4.2.0.0 MOD by odem2014
created by [odem2014](https://github.com/odem2014)
* Updated application title/version to `OpenSkin Designer MOD by odem2014 v4.2.0.0`.
* Added support for `eLabel` `backgroundColor` gradients written as `startColor,endColor,direction`.
* Added support for HEX gradient syntax, including `#00101010,#00303030,vertical` and `#00ff0000,#0000ff00,vertical`.
* Added support for named-color gradients, for example `red,green,vertical`.
* Fixed color parsing so comma-separated gradient values are not treated as invalid single colors.
* Added support for preserving extended `cornerRadius` values such as `30;topLeft,topRight`.
* Fixed saving so gradient `backgroundColor` and masked `cornerRadius` values round-trip correctly.
* Fixed preview rendering so `cornerRadius` edge masks are shown correctly instead of rounding all four corners.
