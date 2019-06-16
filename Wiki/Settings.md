# Overview
Get/Modifies Mod Settings.

# Usage
To see the current value of a setting, do `/settings [Setting]`. To set the value of a setting, do `/settings [Setting] [Value Type]`. To reset settings, do `/settings reset`. Examples:

`/settings shun true` will set ShowUnnecessary to true.

`/settings al true` sets AlwaysUseID to true. Check [Notes](#autocorrect) for more details.

`/settings settings` shows a list of settings.

# Settings
| Setting                             | Shortcut | Value Type | Default | Notes                                                                                                                                                                               |
|-------------------------------------|----------|------------|---------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| ShowProperties<a name="shpr"></a>   | shpr     | bool       | True    | Defines whether the GenerateItem or Set Commands(and/or any other commands that show properties) will show properties after successful execution. Properties Command ignores this.  |
| ShowUnnecessary<a name="shun"></a>  | shun     | bool       | False   | Defines whether unnecessary properties will be shown, eg. prevents createtile from showing when getting Stardust Hammer properties.                                                 |
| ShowEWMessage<a name="shewmsg"></a> | shewmsg  | bool       | True    | Defines whether (ItemModifier)Messages will show when entering the world.                                                                                                           |
| AlwaysUseID<a name="auid"></a>      | auid     | bool       | False   | Defines whether the GenerateItem Command will ignore SetItem and always use user-provided ID.                                                                                       |
| ShowResultList<a name="shrl"></a>   | shrl     | bool       | True    | Defines whether the SetItem Command will show a list of matches when using names.                                                                                                   |
| GetRandomItem<a name="gri"></a>     | gri      | bool       | False   | Defines whether the SetItem Command will get a random item when there is multiple matches.                                                                                          |
| ShowMaxStack<a name="shms"></a>     | shms     | bool       | True    | Defines whether Properties, GenerateItem, and etc. Commands will show the MaxStack property.                                                                                        |

# Notes
Settings are capitalization insensitive. 

Settings automatically update(to the newest version IF you have the newest version) when modifying any setting or doing /settings reset.

<a name="autocorrect"></a>
(Updates following 1.0.8) In efforts of enhancing performance(even by a few megabytes), the AutoCorrect system has been reduced to only check for whether the Setting's Name starts with the User Specified one, eg `/settings al` will return the value of AlwaysUseID while doing `/settings prop` will no longer work and return an error.

>**DEPRECATED/REMOVED** (Prior to 1.0.8)~~The Settings Command has a sort of autocorrect, doing `/setting prop true` will check for any setting containing the phrase "prop", in this case being ShowProperties, and set it to true.~~