OpenSkin Designer MOD by odem2014
=====================================
## Changelog

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

### 3.2.0.0 (08.04.2019)
created by [Scrounger](https://github.com/Scrounger)
* Converter: support for 'FullDescription' added
* Resize picon on element size change
* Use attribute scale for ePixmap & widget which have 'pixmap' attribute.
* Converter MovieInfo added
* Show images for widgets with any render and 'path' attribute
* Show EventImage if render attribute contains 'eventimage'
* Show XHDPicon if render attribute contains 'xhdpicon'
* Show images with 'pixmaps' attribute

### 3.2.2.0 (08.04.2019 - 21.04.2019)
created by [Scrounger](https://github.com/Scrounger)
* cConverterSimplePresets added
* Alias font bug fixes -> gobal loading / usage added
* Fonts sorting added
* Label: font bug fix property grid -> change font or fontsize
* ListBox font added to property grid
* Show font style and size for listboxes
* Font bug fix -> catch exception if font is not defined or exist
* ListBox: Show entries added
* Label metrixreloadedvrunningtext added
* ListBox: count of entries to show bug fixed
* sAttributePixmap: element with attribute 'path' -> bug fix if skinPath is part of attribute path
* converterSimple.xml: MetrixReloaded converters added

### 3.2.3.0 (23.03.2020)
created by [Humaxx](https://github.com/Humaxx)
* Undefined colors are added alternatively ('#' is not replaced by 'un')'
* Added a option how to add undefined colors (with '#' or with 'un')
* Fixed unhandled exception if a borderset-file isn't existing
* Fixed unhandled exception in 'Windowstyle-preview' if no borderstyle is declared in skin.xml
* Bug fix in 'Windowstyle-preview': Now displaying correct borderset and filename
* Fixed a bug that probably exists since 3.1.0.3. Font preview is now again working
* Editor: now showing up to 99999 line numbers instead of max 999
* Editor: background color changed for better contrast
* Text-preview: using lcd.ttf if declared font is not found
* Added VTi-Fonts
* Converter bug fixes: 'TimeshiftService' added to prevent a exception in 'Timeshiftstate'
* Corrected xhdpicon.png for building in visual studio

### 3.2.3.1 (23.03.2020)
created by [Humaxx](https://github.com/Humaxx)
* Added more sources rendered as listbox
* Fixed unhandled exception if source = null

### 3.2.3.2 (26.03.2020)
created by [Humaxx](https://github.com/Humaxx)
* Fixed unhandled exception if no Font is declared or only alias - then using 'lcd.ttf'
* Fixed unhandled exceptions if a color is missing or declared with 'foregroundColors'
* Ask to show messageboxes again or not
* Bugfix: show picon also when a path is set
* Added option to set 'Fallback-Color', which is used for previewing some text

### 3.2.3.3 (27.03.2020)
created by [Humaxx](https://github.com/Humaxx)
* Fixed path not found exception
* Updated converter.xml
* Added speedyAXBlueRunningText
* Removed doubled attributs
* Added some entries to attribut-list like 'foregroundColors' 'options' 'pixmaps' and more
* Added a option to enable showing full attribut-list
* Autocomplete attribut list - max preview set to 15 instead of 5

### 3.2.3.4 (01.04.2020)
created by [Humaxx](https://github.com/Humaxx)
* Added render 'ChamaeleonRunningText'
* If pixmap have a path without specified filename, take random image
* Bugfix: pixmap path
* Added all renders containing 'runningtext'
* Handling all renders containing 'list' as listbox
* Notifying about unsafed changes

### 3.2.3.5 (14.04.2020)
created by [Humaxx](https://github.com/Humaxx)
* Fixed an unhandled exception if image is corrupt
* Only take 'jpg'; 'jpeg' and 'png' for random picture selection

### 3.2.4.0 (04.06.2020)
created by [Humaxx](https://github.com/Humaxx)
* Fixed typos
* Fixed unhandled exception in 'Color Dialog'
* Allow only valid characters in Textboxes in 'Color Dialog'
* Support for language file (CustomLanguage.lng) in 'xml'-diretory

### 3.2.4.1 (05.06.2020)
created by [Humaxx](https://github.com/Humaxx)
* Add search for searching text in code editor

### 3.2.4.2
created by [Humaxx](https://github.com/Humaxx)
* Upgraded search-function
* Added missing translation
* Fixed text from 'Open-Button' in 'Open-Dialog'

### 3.2.4.3
created by [Humaxx](https://github.com/Humaxx)
* Multilanguage support
* Added missing translation
* Settings are now saved

### 3.2.4.4
created by [Humaxx](https://github.com/Humaxx)
* Added missing translation

### 3.2.4.5
created by [Humaxx](https://github.com/Humaxx)
* Displaying the name of the loaded skin.

### 3.2.4.6
created by [Humaxx](https://github.com/Humaxx)
* Fixed unhandled exception when using right-click in designer
* Add turkish language (thanks to 'audi06)
* Bugfix: restoring language only searches for first language file in languages-directory
* Translate existing element-items after changing language

### 3.2.4.7
created by [Humaxx](https://github.com/Humaxx)
* Add albanian language (thanks to 'kqiqi1')
* Fixed polish language
* Added missing translations
* Bugfix: now displaying an error message if a font is not valid

### 3.2.4.8
created by [Humaxx](https://github.com/Humaxx)
* Add options to show notifications about unsafed changes
* Added missing translations
* Bugfix: Now also a notification is shown, if colors are changed
* Added 'ExtEvent' to converter.xml

### 3.2.4.9
created by [Humaxx](https://github.com/Humaxx)
* Using 'delete'-key to delete select element

### 3.2.5.0
created by [Humaxx](https://github.com/Humaxx)
* Added 'experimental delete-mode'
* Bugfix: Don't delete root-node
* Bugfix: 'Color-Dialog': changed 'Change'-button to 'Rename'-button
* Bugfix: 'Color-Dialog': changing a color now triggers unsafed-changes-notification
* Closing 'Color-Dialog' instead of hiding
* Changes in 'Color-Dialog' now take immediatly effect without the need to save and reload
* Nomore saveing and reloading needed if a color is defined two times.

### 3.2.5.1
created by [Humaxx](https://github.com/Humaxx)
* Bugfix: fixed unhandled exception if file (include) was not found
* Bugfix: fixed unhandled exception if * is used for integer value
* Added an example in converterSimple.xml for converter-preview-text

### 3.2.5.2
created by [Humaxx](https://github.com/Humaxx)
* Added an option for linewrapping in code-editor
* Typos
* Added missing translations

### 3.2.5.3
created by [Humaxx](https://github.com/Humaxx)
* Bugfix: fixed unhandled exception if using delete - key without selected item
* Bugfix: using delete-key no longer deletes a selected item in propertygrid

### 3.2.5.4
created by [Humaxx](https://github.com/Humaxx)
* Bugfix: Selected Treeviewnode was deleted when pressing any key in Designer-Mode

### 3.2.5.5
created by [Humaxx](https://github.com/Humaxx)
* Add an option to not replace color beginning with '#'

### 3.2.5.6
created by [Humaxx](https://github.com/Humaxx)
Support for QHD (WQHD) and 4K UHD (Ultra HD)

### 3.2.5.7
created by [Humaxx](https://github.com/Humaxx)
* Support for resolution 3200 x 1800
* Fixed unhandled Exception when borderset has no filename

### 3.2.5.8
created by [Humaxx](https://github.com/Humaxx)
* Fixed unhandled Exception when borderset path has not been specified

### 3.2.5.9
created by [Humaxx](https://github.com/Humaxx)
* Fixed borderset - bug
* application will be terminated if a '.svg' graphic is used in the 'borderset's

### 3.2.6.0
created by [Humaxx](https://github.com/Humaxx)
* Undo application termination if a '.svg' graphic is used in the 'borderset's
* If '.svg' graphic is used, the application searches for a corresponding '.png' graphic'

### 3.2.6.1
created by [Humaxx](https://github.com/Humaxx)
* Added an option to hide attribut-list in code-editor
* Updated language-files
* After opening the skin, the main node is displayed in the code editor
* Bugfix: Notification about unsafed changes, hasn't work in every case

### 3.2.6.2
created by [Humaxx](https://github.com/Humaxx)
* Fixed the display of the error message

### 3.2.6.3
created by [Humaxx](https://github.com/Humaxx)
* Fixed typos
* Added dutch translation --> thanks to 'lk1zhm'
* Added new 'Converter.xml and 'simpleConverter.xml'
* Fixed some unhandled Exception when no converter was found

### 3.2.6.4
created by [Humaxx](https://github.com/Humaxx)
* Fixed unhandeld Exception (ignoring 'templates')

### v3.2.6.5
created by [Humaxx](https://github.com/Humaxx)
* Drawing without color, now using fallback color

### v3.2.6.6 by kitte888"
* Added some new attributes for eLabels like backgroundGradient, cornerRadius
* Added some new attributes for render listBox like itemCornerRadius

### v3.3.0.0
created by [Humaxx](https://github.com/Humaxx)
* Focus remains in the selected property grid value
