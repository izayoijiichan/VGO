# VGO

## Resource schema

### resourse

Indicates where the resource is located.

|definition name|description|type|required|
|:---|:---|:---|:---:|
|uri|The uri of the resource.|string|true|
|byteLength|The length of the resource in bytes.|string|true|

Not used by default.  
Use only if Resource chunk type ID is `REPJ` or `REPB`.


### resourseAccessor

An accessor of the resource.

|definition name|description|type|required|setting value|default value|
|:---|:---|:---|:---:|:---|:---:|
|kind|The kind of the resource accessor.|enum||0: None<br>1: ImageData<br>2: NodeTransform<br>3: MeshData<br>4: SkinData<br>5: ClothCoefficients|0|
|byteOffset|The offset relative to the start of the resource in bytes.|int|true|||
|byteLength|The total byte length of this attribute.|int|||0|
|byteStride|The stride, in bytes.|int|||0|
|dataType|The data type of this attribute.|enum|true|||
|count|The number of attributes referenced by this accessor.|int|true|||
|sparseType|The type of the sparse.|enum||0: None<br>1: General<br>2: Powerful|0|
|sparseCount|Number of entries stored in the sparse array.|int||||
|sparseIndexDataType|The indices data type.|enum||16: UnsignedByte(0x10)<br>32: UnsignedShort(0x20)<br>48: UnsignedInt(0x30)||
|sparseValueDataType|The values data type.|enum||||
|sparseValueOffset|The relative offset for this accessor of sparse value.|int||||

#### resourseAccessor.dataType

This is used in `dataType`, `sparseIndexDataType` and `sparseValueDataType`.

enum

|definition name|decimal|hex|
|:---|---:|---:|
|None|0|0x00|
|UnsignedByte|16|0x10|
|SignedByte|17|0x11|
|UnsignedShort|32|0x20|
|Short|33|0x21|
|UnsignedInt|48|0x30|
|SignedInt|49|0x31|
|Float|80|0x50|
|Vector2Int32|4657|0x1231|
|Vector2Float|4688|0x1250|
|Vector3Int32|4913|0x1331|
|Vector3Float|4944|0x1350|
|Vector4UInt8|5136|0x1410|
|Vector4UInt16|5152|0x1420|
|Vector4UInt32|5168|0x1430|
|Vector4Int32|5169|0x1431|
|Vector4Float|5200|0x1450|
|Matrix4Float|9296|0x2450|

#### Sparse Type

[General]

This is a method of compressing data by creating a list that extracts only data values other than the default value (0).

[Powerful]

This is a more powerful compression method for `General`.

For example, if the data is a series of vector3 arrays, the whole is regarded as a float array and sparse compression is performed.  
Unlike in the case of `General`, sparseCount can be greater than accessorCount.


### JSON example (resourseAccessors)

```json
[
    {
        "kind": 1,
        "byteOffset": 0,
        "byteLength": 605083,
        "byteStride": 1,
        "dataType": 16,
        "count": 605083
    },
    {
        "kind": 1,
        "byteOffset": 605083,
        "byteLength": 184903,
        "byteStride": 1,
        "dataType": 16,
        "count": 184903
    },
    {
        "kind": 2,
        "byteOffset": 789986,
        "byteLength": 384,
        "byteStride": 64,
        "dataType": 9296,
        "count": 6
    },
    {
        "kind": 3,
        "byteOffset": 790370,
        "byteLength": 288,
        "byteStride": 12,
        "dataType": 4944,
        "count": 24
    },
    {
        "kind": 3,
        "byteOffset": 790658,
        "byteLength": 288,
        "byteStride": 12,
        "dataType": 4944,
        "count": 24
    },
    ...
    {
        "kind": 3,
        "byteOffset" : 4363605,
        "byteLength" : 9926,
        "dataType" : 4944,
        "count" : 1894,
        "sparseType" : 1,
        "sparseCount" : 709,
        "sparseIndexDataType" : 32,
        "sparseValueDataType" : 4944,
        "sparseValueByteOffset" : 1418
    },
    ...
    {
        "kind": 3,
        "byteOffset": 5317922,
        "byteLength": 9156,
        "dataType": 4944,
        "count": 1894,
        "sparseType": 2,
        "sparseCount": 1526,
        "sparseIndexDataType": 32,
        "sparseValueDataType": 80,
        "sparseValueByteOffset": 3052
    }
]
```
___
Last updated: 5 June, 2020  
Editor: Izayoi Jiichan

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
