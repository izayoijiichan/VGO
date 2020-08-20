# VGO

## Chunk overview

### Chunk

All blocks are built with IFF chunks.

|offset|length|name|
|:--:|:--:|:--|
|0|4|Type ID|
|4|4|Data Length|
|8|n|Data|

### Chunk group


|type group|name|description|required|
|:--|:--|:--|:--:|
|VGO|Header|File header.|true|
|IDX|Index|Holds the index of the chunk.|true|
|COMP|Composer|Holds a combination of chunks to build a 3D model.|true|
|AIXX|Asset Info|Holds asset information.|true|
|LAXX|Layout|Holds 3D model layout information.|true|
|RAXX|Resource Accessor|Holds access information to the resource.|true|
|REXX|Resource|Holds a resource.|true|
|CXXX|Crypt|Holds cryptographic information.||

### Chunk type ID

|index|type ID|hex|name|plain/crypt|data format|
|:--:|:--|:--:|:--|:--:|:--:|
|0|VGO|0x004F4756|The file magic.|plain|binary|
|1|IDX|0x00584449|The index chunk.|plain|binary|
|2|COMP|0x504D4F43|The composer.|plain|binary|
|(3)|AIPJ|0x4A504941|The asset info.|plain|JSON|
||AIPB|0x42504941|The asset info.|plain|BSON|
|(4)|LAPJ|0x4A50414C|The layout.|plain|JSON|
||LAPB|0x4250414C|The layout.|plain|BSON|
|(5)|RAPJ|0x4A504152|The resource accsessor.|plain|JSON|
||RAPB|0x42504152|The resource accsessor.|plain|BSON|
||RACJ|0x4A434152|The resource accsessor.|crypt|JSON|
||RACB|0x42434152|The resource accsessor.|crypt|BSON|
|(6)|CRAJ|0x4A415243|The ctypt info for resource accsessor.|plain|JSON|
||CRAB|0x42415243|The ctypt info for resource accsessor.|plain|BSON|
|(7)|REPb|0x62504552|The resource.|plain|binary|
||REPJ|0x4A504552|The resource.|plain|JSON|
||REPB|0x42504552|The resource.|plain|BSON|

### Encryption

Partial encryption is applied.  
In the current version, it is valid only for the data part of the resource accessor.  
When the key is embedded inside, it is not a completely secure encryption.

|algorythm|parameters|remarks|
|:--|:--|:--|
|AES|key, iv, cipherMode, paddingMode||
|Base64||not secure|

___
## Chunk details

### Header chunk

16-byte

|offset|length|data type|name|description|requred|value|
|:--:|:--:|:--:|:--|:--|:--:|:--|
|0|4|unsigned int|Magic|File magic.|true|VGO|
|4|4|unsigned int|DataLength|Length of chunk data.|true|8|
|8|1|unsigned byte|MajorVersion|Major version of VGO.|true|2|
|9|1|unsigned byte|MinorVersion|Minor version of VGO.|true|0|
|10|1|unsigned byte|GeometryCoordinate|Geometry coordinates.|true|1: Right Hand<br>2: Left Hand|
|11|1|unsigned byte|UVCoordinate|UV coorinates.|true|1: Top Left<br>2: Bottom Left|
|12|1|unsigned byte|IsCrypted|Whether the resource accessor is crypted.|true|0: not crypted<br>1: crypted|
|13|1|unsigned byte|IsRequireExternalCryptKey|Whether the resource accessor external crypt key is required.|true|0: not require<br>1: require|
|14|1|unsigned byte|(Reserved)||||
|15|1|unsigned byte|(Reserved)||||

### Index chunk

|offset|length|data type|name|description|requred|value|
|:--:|:--:|:--:|:--|:--|:--:|:--:|
|0|4|unsigned int|TypeId|Chunk type ID.|true|IDX|
|4|4|unsigned int|DataLength|Length of chunk data.|true||
|8|16 * n|VgoIndexChunkDataElement[]||Chunk index information.|true||

#### VgoIndexChunkDataElement

16-byte

|offset|length|data type|name|description|requred|value|
|:--:|:--:|:--:|:--|:--|:--:|:--:|
|0|4|unsigned int|ChunkTypeId|Chunk type ID.|true||
|4|4|unsigned int|ByteOffset|Start position of chunk.|true||
|8|4|unsigned int|ByteLength|Total chunk length. (Including padding)|true||
|12|1|unsigned byte|BytePadding|Number of bytes of padding for chunk data.|true|0 or 1|
|13|1|unsigned byte|(Reserved)||||
|14|1|unsigned byte|(Reserved)||||
|15|1|unsigned byte|(Reserved)||||

### Component chunk

40-byte

|offset|length|data type|name|description|requred|value|
|:--:|:--:|:--:|:--|:--|:--:|:--:|
|0|4|unsigned int|TypeId|Chunk type ID.|true|COMP|
|4|4|unsigned int|DataLength|Length of chunk data.|true|32|
|8|32|VgoComponentChunkData|||true||

#### VgoComponentChunkData

32-byte

|offset|length|data type|name|description|requred|value|
|:--:|:--:|:--:|:--|:--|:--:|:--:|
|0|4|unsigned int|AssetInfoChunkTypeId|Asset information chunk type ID.|true|AIXX|
|4|4|unsigned int|(Reserved)||||
|8|4|unsigned int|LayoutChunkTypeId|Layout chunk type ID.|true|LAXX|
|12|4|unsigned int|(Reserved)||||
|16|4|unsigned int|ResourceAccessorChunkTypeId|Resource accessor chunk type ID.|true|RAXX|
|20|4|unsigned int|ResourceAccessorCryptChunkTypeId|Resource accessor crypto chunk type ID.|false|CRAX|
|24|4|unsigned int|ResourceChunkTypeId|Resource chunk type ID.|true|REXX|
|28|4|unsigned int|(Reserved)||||

### Asset Info chunk

8 + n byte

|offset|length|data type|name|description|requred|value|
|:--:|:--:|:--:|:--|:--|:--:|:--:|
|0|4|unsigned int|TypeId|Chunk type ID.|true|AIXX|
|4|4|unsigned int|DataLength|Length of chunk data.|true|n|
|8|n|byte[]|Data|Asset information.|true|JSON or BSON|

### Layout chunk

8 + n byte

|offset|length|data type|name|description|requred|value|
|:--:|:--:|:--:|:--|:--|:--:|:--:|
|0|4|unsigned int|TypeId|Chunk type ID.|true|LAXX|
|4|4|unsigned int|DataLength|Length of chunk data.|true|n|
|8|n|byte[]|Data|Layout data.|true|JSON or BSON|

### Resoure Accessor chunk

8 + n byte

|offset|length|data type|name|description|requred|value|
|:--:|:--:|:--:|:--|:--|:--:|:--:|
|0|4|unsigned int|TypeId|Chunk type ID.|true|RAXX|
|4|4|unsigned int|DataLength|Length of chunk data.|true|n|
|8|n|byte[]|Data|Resource accessor data.|true|JSON or BSON (plain or crypted)|

### Resoure chunk

8 + n byte

|offset|length|data type|name|description|requred|value|
|:--:|:--:|:--:|:--|:--|:--:|:--:|
|0|4|unsigned int|TypeId|Chunk type ID.|true|REXX|
|4|4|unsigned int|DataLength|Length of chunk data.|true|n|
|8|n|byte[]|Data|Resource data.|true|binary or JSON or BSON|

___
Last updated: 20 August, 2020  
Editor: Izayoi Jiichan

*Copyright (C) 2020 Izayoi Jiichan. All Rights Reserved.*
