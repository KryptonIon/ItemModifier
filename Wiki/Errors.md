# Errors
Updates following 1.0.8 have an Error System that assigns codes to most errors, this is for the sake of explanation and solutions.

### Error 0, Parsing Error
Message : `Error 0. {varName}(UserSpecified:"{value}") must be {type}`

VarName : Indicates where the error occurred.

Value : What the user specified, eg. "false".

| Type         | Example Values     |
|--------------|--------------------|
| Integer(int) | 1, 2, 3, 4, etc.   |
| Float        | 1.2, 5.6, 10, etc. |
| Bool         | True / False       |

Example Error : `Error 0. ShootSpeed(UserSpecified:"false") must be int`

Solution : Specify the correct value type.

### Error 1, Negative Error
Message : `Error 1. {varName}(UserSpecified:"{value}") can't be negative`

VarName : Indicates where the error occured.

Value : What the user-specified, eg. -10.

Example Error : `Error 1. Shoot(UserSpecified:"-10") can't be negative`

Solution : Use a positive value.

### Error 2, Bigger than Error
Message : `Error 2. {varName}(UserSpecified:"{value}") must be smaller than {maxName}({max})`

VarName : Indicates where the error occured.

Value : What the user-specified, eg. 999999.

MaxName : The name of the value that Value surpassed.

Max : The value that Value surpassed.

Example Error : `Error 2. CreateTile(UserSpecified:"999999") must be smaller than TileID(469)`

Solution : Use a value smaller than the one mentioned in the error.

> **DEPRECATED/NOT USED**
> ### Error 3, Null Error
> Message : `Error 3. {varName} is {isName}`
> 
> VarName : Indicates where the error occured.
> 
> IsName : Indicates what the value is.
> 
> Example Error : `Error 3. ItemID is air`

### Error 4, No Match Error
Message : `Error 4. "{name}" doesn't match anything`

Name : Indicates the user specified string/value.

Example Error : `Error 4. "shot meteor" doesn't match anything`

Meaning : Provided/Specified string doesn't match anything

### Error 4, Invalid Setting Error
Message : `Error 4. "{settingName}" is an invalid setting`

SettingName : Indicates the user specified string/value.

Example Error : `Error 4. "Debug" is an invalid setting`

Meaning : Provided/Specified string doesn't match anything

### Error 4, Invalid Argument Error
Message : `Error 4. "{argumentName}" is an invalid argument`

argumentName : Indicates the user specified string/value.

Example Error : `Error 4. "speedshoot" is an invalid argument`

Meaning : Provided/Specified string doesn't match anything