var Deserializers = {}
Deserializers["UnityEngine.JointSpring"] = function (request, data, root) {
  var i2140 = root || request.c( 'UnityEngine.JointSpring' )
  var i2141 = data
  i2140.spring = i2141[0]
  i2140.damper = i2141[1]
  i2140.targetPosition = i2141[2]
  return i2140
}

Deserializers["UnityEngine.JointMotor"] = function (request, data, root) {
  var i2142 = root || request.c( 'UnityEngine.JointMotor' )
  var i2143 = data
  i2142.m_TargetVelocity = i2143[0]
  i2142.m_Force = i2143[1]
  i2142.m_FreeSpin = i2143[2]
  return i2142
}

Deserializers["UnityEngine.JointLimits"] = function (request, data, root) {
  var i2144 = root || request.c( 'UnityEngine.JointLimits' )
  var i2145 = data
  i2144.m_Min = i2145[0]
  i2144.m_Max = i2145[1]
  i2144.m_Bounciness = i2145[2]
  i2144.m_BounceMinVelocity = i2145[3]
  i2144.m_ContactDistance = i2145[4]
  i2144.minBounce = i2145[5]
  i2144.maxBounce = i2145[6]
  return i2144
}

Deserializers["UnityEngine.JointDrive"] = function (request, data, root) {
  var i2146 = root || request.c( 'UnityEngine.JointDrive' )
  var i2147 = data
  i2146.m_PositionSpring = i2147[0]
  i2146.m_PositionDamper = i2147[1]
  i2146.m_MaximumForce = i2147[2]
  i2146.m_UseAcceleration = i2147[3]
  return i2146
}

Deserializers["UnityEngine.SoftJointLimitSpring"] = function (request, data, root) {
  var i2148 = root || request.c( 'UnityEngine.SoftJointLimitSpring' )
  var i2149 = data
  i2148.m_Spring = i2149[0]
  i2148.m_Damper = i2149[1]
  return i2148
}

Deserializers["UnityEngine.SoftJointLimit"] = function (request, data, root) {
  var i2150 = root || request.c( 'UnityEngine.SoftJointLimit' )
  var i2151 = data
  i2150.m_Limit = i2151[0]
  i2150.m_Bounciness = i2151[1]
  i2150.m_ContactDistance = i2151[2]
  return i2150
}

Deserializers["UnityEngine.WheelFrictionCurve"] = function (request, data, root) {
  var i2152 = root || request.c( 'UnityEngine.WheelFrictionCurve' )
  var i2153 = data
  i2152.m_ExtremumSlip = i2153[0]
  i2152.m_ExtremumValue = i2153[1]
  i2152.m_AsymptoteSlip = i2153[2]
  i2152.m_AsymptoteValue = i2153[3]
  i2152.m_Stiffness = i2153[4]
  return i2152
}

Deserializers["UnityEngine.JointAngleLimits2D"] = function (request, data, root) {
  var i2154 = root || request.c( 'UnityEngine.JointAngleLimits2D' )
  var i2155 = data
  i2154.m_LowerAngle = i2155[0]
  i2154.m_UpperAngle = i2155[1]
  return i2154
}

Deserializers["UnityEngine.JointMotor2D"] = function (request, data, root) {
  var i2156 = root || request.c( 'UnityEngine.JointMotor2D' )
  var i2157 = data
  i2156.m_MotorSpeed = i2157[0]
  i2156.m_MaximumMotorTorque = i2157[1]
  return i2156
}

Deserializers["UnityEngine.JointSuspension2D"] = function (request, data, root) {
  var i2158 = root || request.c( 'UnityEngine.JointSuspension2D' )
  var i2159 = data
  i2158.m_DampingRatio = i2159[0]
  i2158.m_Frequency = i2159[1]
  i2158.m_Angle = i2159[2]
  return i2158
}

Deserializers["UnityEngine.JointTranslationLimits2D"] = function (request, data, root) {
  var i2160 = root || request.c( 'UnityEngine.JointTranslationLimits2D' )
  var i2161 = data
  i2160.m_LowerTranslation = i2161[0]
  i2160.m_UpperTranslation = i2161[1]
  return i2160
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Transform"] = function (request, data, root) {
  var i2162 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Transform' )
  var i2163 = data
  i2162.position = new pc.Vec3( i2163[0], i2163[1], i2163[2] )
  i2162.scale = new pc.Vec3( i2163[3], i2163[4], i2163[5] )
  i2162.rotation = new pc.Quat(i2163[6], i2163[7], i2163[8], i2163[9])
  return i2162
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.SpriteRenderer"] = function (request, data, root) {
  var i2164 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.SpriteRenderer' )
  var i2165 = data
  i2164.enabled = !!i2165[0]
  request.r(i2165[1], i2165[2], 0, i2164, 'sharedMaterial')
  var i2167 = i2165[3]
  var i2166 = []
  for(var i = 0; i < i2167.length; i += 2) {
  request.r(i2167[i + 0], i2167[i + 1], 2, i2166, '')
  }
  i2164.sharedMaterials = i2166
  i2164.receiveShadows = !!i2165[4]
  i2164.shadowCastingMode = i2165[5]
  i2164.sortingLayerID = i2165[6]
  i2164.sortingOrder = i2165[7]
  i2164.lightmapIndex = i2165[8]
  i2164.lightmapSceneIndex = i2165[9]
  i2164.lightmapScaleOffset = new pc.Vec4( i2165[10], i2165[11], i2165[12], i2165[13] )
  i2164.lightProbeUsage = i2165[14]
  i2164.reflectionProbeUsage = i2165[15]
  i2164.color = new pc.Color(i2165[16], i2165[17], i2165[18], i2165[19])
  request.r(i2165[20], i2165[21], 0, i2164, 'sprite')
  i2164.flipX = !!i2165[22]
  i2164.flipY = !!i2165[23]
  i2164.drawMode = i2165[24]
  i2164.size = new pc.Vec2( i2165[25], i2165[26] )
  i2164.tileMode = i2165[27]
  i2164.adaptiveModeThreshold = i2165[28]
  i2164.maskInteraction = i2165[29]
  i2164.spriteSortPoint = i2165[30]
  return i2164
}

Deserializers["SpriteCornerDebugger"] = function (request, data, root) {
  var i2170 = root || request.c( 'SpriteCornerDebugger' )
  var i2171 = data
  i2170.gizmoColor = new pc.Color(i2171[0], i2171[1], i2171[2], i2171[3])
  i2170.gizmoSize = i2171[4]
  request.r(i2171[5], i2171[6], 0, i2170, 'generalGameSettingSO')
  return i2170
}

Deserializers["Luna.Unity.DTO.UnityEngine.Scene.GameObject"] = function (request, data, root) {
  var i2172 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Scene.GameObject' )
  var i2173 = data
  i2172.name = i2173[0]
  i2172.tagId = i2173[1]
  i2172.enabled = !!i2173[2]
  i2172.isStatic = !!i2173[3]
  i2172.layer = i2173[4]
  return i2172
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material"] = function (request, data, root) {
  var i2174 = root || new pc.UnityMaterial()
  var i2175 = data
  i2174.name = i2175[0]
  request.r(i2175[1], i2175[2], 0, i2174, 'shader')
  i2174.renderQueue = i2175[3]
  i2174.enableInstancing = !!i2175[4]
  var i2177 = i2175[5]
  var i2176 = []
  for(var i = 0; i < i2177.length; i += 1) {
    i2176.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter', i2177[i + 0]) );
  }
  i2174.floatParameters = i2176
  var i2179 = i2175[6]
  var i2178 = []
  for(var i = 0; i < i2179.length; i += 1) {
    i2178.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter', i2179[i + 0]) );
  }
  i2174.colorParameters = i2178
  var i2181 = i2175[7]
  var i2180 = []
  for(var i = 0; i < i2181.length; i += 1) {
    i2180.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter', i2181[i + 0]) );
  }
  i2174.vectorParameters = i2180
  var i2183 = i2175[8]
  var i2182 = []
  for(var i = 0; i < i2183.length; i += 1) {
    i2182.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter', i2183[i + 0]) );
  }
  i2174.textureParameters = i2182
  var i2185 = i2175[9]
  var i2184 = []
  for(var i = 0; i < i2185.length; i += 1) {
    i2184.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag', i2185[i + 0]) );
  }
  i2174.materialFlags = i2184
  return i2174
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter"] = function (request, data, root) {
  var i2188 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter' )
  var i2189 = data
  i2188.name = i2189[0]
  i2188.value = i2189[1]
  return i2188
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter"] = function (request, data, root) {
  var i2192 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter' )
  var i2193 = data
  i2192.name = i2193[0]
  i2192.value = new pc.Color(i2193[1], i2193[2], i2193[3], i2193[4])
  return i2192
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter"] = function (request, data, root) {
  var i2196 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter' )
  var i2197 = data
  i2196.name = i2197[0]
  i2196.value = new pc.Vec4( i2197[1], i2197[2], i2197[3], i2197[4] )
  return i2196
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter"] = function (request, data, root) {
  var i2200 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter' )
  var i2201 = data
  i2200.name = i2201[0]
  request.r(i2201[1], i2201[2], 0, i2200, 'value')
  return i2200
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag"] = function (request, data, root) {
  var i2204 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag' )
  var i2205 = data
  i2204.name = i2205[0]
  i2204.enabled = !!i2205[1]
  return i2204
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.BoxCollider2D"] = function (request, data, root) {
  var i2206 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.BoxCollider2D' )
  var i2207 = data
  i2206.usedByComposite = !!i2207[0]
  i2206.autoTiling = !!i2207[1]
  i2206.size = new pc.Vec2( i2207[2], i2207[3] )
  i2206.edgeRadius = i2207[4]
  i2206.enabled = !!i2207[5]
  i2206.isTrigger = !!i2207[6]
  i2206.usedByEffector = !!i2207[7]
  i2206.density = i2207[8]
  i2206.offset = new pc.Vec2( i2207[9], i2207[10] )
  request.r(i2207[11], i2207[12], 0, i2206, 'material')
  return i2206
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Texture2D"] = function (request, data, root) {
  var i2208 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Texture2D' )
  var i2209 = data
  i2208.name = i2209[0]
  i2208.width = i2209[1]
  i2208.height = i2209[2]
  i2208.mipmapCount = i2209[3]
  i2208.anisoLevel = i2209[4]
  i2208.filterMode = i2209[5]
  i2208.hdr = !!i2209[6]
  i2208.format = i2209[7]
  i2208.wrapMode = i2209[8]
  i2208.alphaIsTransparency = !!i2209[9]
  i2208.alphaSource = i2209[10]
  i2208.graphicsFormat = i2209[11]
  i2208.sRGBTexture = !!i2209[12]
  i2208.desiredColorSpace = i2209[13]
  i2208.wrapU = i2209[14]
  i2208.wrapV = i2209[15]
  return i2208
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.RectTransform"] = function (request, data, root) {
  var i2210 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.RectTransform' )
  var i2211 = data
  i2210.pivot = new pc.Vec2( i2211[0], i2211[1] )
  i2210.anchorMin = new pc.Vec2( i2211[2], i2211[3] )
  i2210.anchorMax = new pc.Vec2( i2211[4], i2211[5] )
  i2210.sizeDelta = new pc.Vec2( i2211[6], i2211[7] )
  i2210.anchoredPosition3D = new pc.Vec3( i2211[8], i2211[9], i2211[10] )
  i2210.rotation = new pc.Quat(i2211[11], i2211[12], i2211[13], i2211[14])
  i2210.scale = new pc.Vec3( i2211[15], i2211[16], i2211[17] )
  return i2210
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer"] = function (request, data, root) {
  var i2212 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer' )
  var i2213 = data
  i2212.cullTransparentMesh = !!i2213[0]
  return i2212
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.CanvasGroup"] = function (request, data, root) {
  var i2214 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.CanvasGroup' )
  var i2215 = data
  i2214.m_Alpha = i2215[0]
  i2214.m_Interactable = !!i2215[1]
  i2214.m_BlocksRaycasts = !!i2215[2]
  i2214.m_IgnoreParentGroups = !!i2215[3]
  i2214.enabled = !!i2215[4]
  return i2214
}

Deserializers["UnityEngine.UI.Image"] = function (request, data, root) {
  var i2216 = root || request.c( 'UnityEngine.UI.Image' )
  var i2217 = data
  request.r(i2217[0], i2217[1], 0, i2216, 'm_Sprite')
  i2216.m_Type = i2217[2]
  i2216.m_PreserveAspect = !!i2217[3]
  i2216.m_FillCenter = !!i2217[4]
  i2216.m_FillMethod = i2217[5]
  i2216.m_FillAmount = i2217[6]
  i2216.m_FillClockwise = !!i2217[7]
  i2216.m_FillOrigin = i2217[8]
  i2216.m_UseSpriteMesh = !!i2217[9]
  i2216.m_PixelsPerUnitMultiplier = i2217[10]
  request.r(i2217[11], i2217[12], 0, i2216, 'm_Material')
  i2216.m_Maskable = !!i2217[13]
  i2216.m_Color = new pc.Color(i2217[14], i2217[15], i2217[16], i2217[17])
  i2216.m_RaycastTarget = !!i2217[18]
  i2216.m_RaycastPadding = new pc.Vec4( i2217[19], i2217[20], i2217[21], i2217[22] )
  return i2216
}

Deserializers["Luna.Unity.DTO.UnityEngine.Scene.Scene"] = function (request, data, root) {
  var i2218 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Scene.Scene' )
  var i2219 = data
  i2218.name = i2219[0]
  i2218.index = i2219[1]
  i2218.startup = !!i2219[2]
  return i2218
}

Deserializers["ECS_MagicTile.GlobalPoint"] = function (request, data, root) {
  var i2220 = root || request.c( 'ECS_MagicTile.GlobalPoint' )
  var i2221 = data
  request.r(i2221[0], i2221[1], 0, i2220, 'generalGameSetting')
  request.r(i2221[2], i2221[3], 0, i2220, 'musicNoteCreationSettings')
  request.r(i2221[4], i2221[5], 0, i2220, 'perfectLineSetting')
  request.r(i2221[6], i2221[7], 0, i2220, 'laneLineSettings')
  request.r(i2221[8], i2221[9], 0, i2220, 'OnGameStartChannel')
  request.r(i2221[10], i2221[11], 0, i2220, 'OnScoreHitChannel')
  request.r(i2221[12], i2221[13], 0, i2220, 'OnOrientationChangedChannel')
  request.r(i2221[14], i2221[15], 0, i2220, 'OnSongStartChannel')
  request.r(i2221[16], i2221[17], 0, i2220, 'scoreText')
  request.r(i2221[18], i2221[19], 0, i2220, 'progressSlider')
  request.r(i2221[20], i2221[21], 0, i2220, 'perfectLineObject')
  request.r(i2221[22], i2221[23], 0, i2220, 'mainCamera')
  return i2220
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Camera"] = function (request, data, root) {
  var i2222 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Camera' )
  var i2223 = data
  i2222.enabled = !!i2223[0]
  i2222.aspect = i2223[1]
  i2222.orthographic = !!i2223[2]
  i2222.orthographicSize = i2223[3]
  i2222.backgroundColor = new pc.Color(i2223[4], i2223[5], i2223[6], i2223[7])
  i2222.nearClipPlane = i2223[8]
  i2222.farClipPlane = i2223[9]
  i2222.fieldOfView = i2223[10]
  i2222.depth = i2223[11]
  i2222.clearFlags = i2223[12]
  i2222.cullingMask = i2223[13]
  i2222.rect = i2223[14]
  request.r(i2223[15], i2223[16], 0, i2222, 'targetTexture')
  i2222.usePhysicalProperties = !!i2223[17]
  i2222.focalLength = i2223[18]
  i2222.sensorSize = new pc.Vec2( i2223[19], i2223[20] )
  i2222.lensShift = new pc.Vec2( i2223[21], i2223[22] )
  i2222.gateFit = i2223[23]
  i2222.commandBufferCount = i2223[24]
  i2222.cameraType = i2223[25]
  return i2222
}

Deserializers["PerfectLineCameraSpacePositionAdjuster"] = function (request, data, root) {
  var i2224 = root || request.c( 'PerfectLineCameraSpacePositionAdjuster' )
  var i2225 = data
  request.r(i2225[0], i2225[1], 0, i2224, 'targetCamera')
  request.r(i2225[2], i2225[3], 0, i2224, 'perfectLineSetting')
  i2224.portraitNormalizedPos = request.d('ECS_MagicTile.PerfectLineSetting+NormalizedFloatPreset', i2225[4], i2224.portraitNormalizedPos)
  i2224.landscapeNormalizedPos = request.d('ECS_MagicTile.PerfectLineSetting+NormalizedFloatPreset', i2225[5], i2224.landscapeNormalizedPos)
  return i2224
}

Deserializers["ECS_MagicTile.PerfectLineSetting+NormalizedFloatPreset"] = function (request, data, root) {
  var i2226 = root || request.c( 'ECS_MagicTile.PerfectLineSetting+NormalizedFloatPreset' )
  var i2227 = data
  i2226.normalizedX = request.d('ECS_MagicTile.RangeReactiveFloat', i2227[0], i2226.normalizedX)
  i2226.normalizedY = request.d('ECS_MagicTile.RangeReactiveFloat', i2227[1], i2226.normalizedY)
  return i2226
}

Deserializers["ECS_MagicTile.RangeReactiveFloat"] = function (request, data, root) {
  var i2228 = root || request.c( 'ECS_MagicTile.RangeReactiveFloat' )
  var i2229 = data
  i2228._value = i2229[0]
  return i2228
}

Deserializers["PerfectLineSpriteResizer"] = function (request, data, root) {
  var i2230 = root || request.c( 'PerfectLineSpriteResizer' )
  var i2231 = data
  request.r(i2231[0], i2231[1], 0, i2230, 'targetCamera')
  request.r(i2231[2], i2231[3], 0, i2230, 'perfectLineSetting')
  i2230.portraitNormalizedSize = request.d('ECS_MagicTile.PerfectLineSetting+NormalizedFloatPreset', i2231[4], i2230.portraitNormalizedSize)
  i2230.landscapeNormalizedSize = request.d('ECS_MagicTile.PerfectLineSetting+NormalizedFloatPreset', i2231[5], i2230.landscapeNormalizedSize)
  i2230.maintainAspectRatio = !!i2231[6]
  return i2230
}

Deserializers["PerfectLineFakeVisual"] = function (request, data, root) {
  var i2232 = root || request.c( 'PerfectLineFakeVisual' )
  var i2233 = data
  request.r(i2233[0], i2233[1], 0, i2232, 'perfectLineSetting')
  request.r(i2233[2], i2233[3], 0, i2232, 'targetCamera')
  request.r(i2233[4], i2233[5], 0, i2232, 'onOrientationChangedChannel')
  return i2232
}

Deserializers["ECS_MagicTile.RaycastToStartGame"] = function (request, data, root) {
  var i2234 = root || request.c( 'ECS_MagicTile.RaycastToStartGame' )
  var i2235 = data
  i2234.targetLayer = UnityEngine.LayerMask.FromIntegerValue( i2235[0] )
  request.r(i2235[1], i2235[2], 0, i2234, 'OnGameStartChannel')
  return i2234
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Canvas"] = function (request, data, root) {
  var i2236 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Canvas' )
  var i2237 = data
  i2236.enabled = !!i2237[0]
  i2236.planeDistance = i2237[1]
  i2236.referencePixelsPerUnit = i2237[2]
  i2236.isFallbackOverlay = !!i2237[3]
  i2236.renderMode = i2237[4]
  i2236.renderOrder = i2237[5]
  i2236.sortingLayerName = i2237[6]
  i2236.sortingOrder = i2237[7]
  i2236.scaleFactor = i2237[8]
  request.r(i2237[9], i2237[10], 0, i2236, 'worldCamera')
  i2236.overrideSorting = !!i2237[11]
  i2236.pixelPerfect = !!i2237[12]
  i2236.targetDisplay = i2237[13]
  i2236.overridePixelPerfect = !!i2237[14]
  return i2236
}

Deserializers["UnityEngine.UI.CanvasScaler"] = function (request, data, root) {
  var i2238 = root || request.c( 'UnityEngine.UI.CanvasScaler' )
  var i2239 = data
  i2238.m_UiScaleMode = i2239[0]
  i2238.m_ReferencePixelsPerUnit = i2239[1]
  i2238.m_ScaleFactor = i2239[2]
  i2238.m_ReferenceResolution = new pc.Vec2( i2239[3], i2239[4] )
  i2238.m_ScreenMatchMode = i2239[5]
  i2238.m_MatchWidthOrHeight = i2239[6]
  i2238.m_PhysicalUnit = i2239[7]
  i2238.m_FallbackScreenDPI = i2239[8]
  i2238.m_DefaultSpriteDPI = i2239[9]
  i2238.m_DynamicPixelsPerUnit = i2239[10]
  i2238.m_PresetInfoIsWorld = !!i2239[11]
  return i2238
}

Deserializers["UnityEngine.UI.GraphicRaycaster"] = function (request, data, root) {
  var i2240 = root || request.c( 'UnityEngine.UI.GraphicRaycaster' )
  var i2241 = data
  i2240.m_IgnoreReversedGraphics = !!i2241[0]
  i2240.m_BlockingObjects = i2241[1]
  i2240.m_BlockingMask = UnityEngine.LayerMask.FromIntegerValue( i2241[2] )
  return i2240
}

Deserializers["ECS_MagicTile.ScoreEffectController"] = function (request, data, root) {
  var i2242 = root || request.c( 'ECS_MagicTile.ScoreEffectController' )
  var i2243 = data
  request.r(i2243[0], i2243[1], 0, i2242, 'scoreSignalEffectChannel')
  request.r(i2243[2], i2243[3], 0, i2242, 'perfectScorePrefab')
  request.r(i2243[4], i2243[5], 0, i2242, 'greatScorePrefab')
  request.r(i2243[6], i2243[7], 0, i2242, 'burstMovementUIController')
  var i2245 = i2243[8]
  var i2244 = []
  for(var i = 0; i < i2245.length; i += 1) {
    i2244.push( request.d('BurstMovementUIController+BurstMovementElement', i2245[i + 0]) );
  }
  i2242.burstMovementElements = i2244
  return i2242
}

Deserializers["BurstMovementUIController+BurstMovementElement"] = function (request, data, root) {
  var i2248 = root || request.c( 'BurstMovementUIController+BurstMovementElement' )
  var i2249 = data
  request.r(i2249[0], i2249[1], 0, i2248, 'target')
  i2248.data = request.d('BurstMovementUIController+BurstMovementData', i2249[2], i2248.data)
  return i2248
}

Deserializers["BurstMovementUIController+BurstMovementData"] = function (request, data, root) {
  var i2250 = root || request.c( 'BurstMovementUIController+BurstMovementData' )
  var i2251 = data
  i2250.direction = new pc.Vec2( i2251[0], i2251[1] )
  i2250.maxDistance = i2251[2]
  i2250.maxSpeed = i2251[3]
  i2250.accelerationFactor = i2251[4]
  i2250.decelerationFactor = i2251[5]
  i2250.burstEndPercentage = i2251[6]
  i2250.initialSpeedPercent = i2251[7]
  i2250.startPosition = new pc.Vec2( i2251[8], i2251[9] )
  i2250.currentSpeed = i2251[10]
  i2250.hasStarted = !!i2251[11]
  i2250.isFinished = !!i2251[12]
  return i2250
}

Deserializers["BurstMovementUIController"] = function (request, data, root) {
  var i2252 = root || request.c( 'BurstMovementUIController' )
  var i2253 = data
  i2252.defaultMaxSpeed = i2253[0]
  i2252.defaultAccelerationFactor = i2253[1]
  i2252.defaultDecelerationFactor = i2253[2]
  i2252.defaultBurstEndPercentage = i2253[3]
  i2252.defaultInitialSpeedPercent = i2253[4]
  return i2252
}

Deserializers["UnityEngine.UI.Text"] = function (request, data, root) {
  var i2254 = root || request.c( 'UnityEngine.UI.Text' )
  var i2255 = data
  i2254.m_FontData = request.d('UnityEngine.UI.FontData', i2255[0], i2254.m_FontData)
  i2254.m_Text = i2255[1]
  request.r(i2255[2], i2255[3], 0, i2254, 'm_Material')
  i2254.m_Maskable = !!i2255[4]
  i2254.m_Color = new pc.Color(i2255[5], i2255[6], i2255[7], i2255[8])
  i2254.m_RaycastTarget = !!i2255[9]
  i2254.m_RaycastPadding = new pc.Vec4( i2255[10], i2255[11], i2255[12], i2255[13] )
  return i2254
}

Deserializers["UnityEngine.UI.FontData"] = function (request, data, root) {
  var i2256 = root || request.c( 'UnityEngine.UI.FontData' )
  var i2257 = data
  request.r(i2257[0], i2257[1], 0, i2256, 'm_Font')
  i2256.m_FontSize = i2257[2]
  i2256.m_FontStyle = i2257[3]
  i2256.m_BestFit = !!i2257[4]
  i2256.m_MinSize = i2257[5]
  i2256.m_MaxSize = i2257[6]
  i2256.m_Alignment = i2257[7]
  i2256.m_AlignByGeometry = !!i2257[8]
  i2256.m_RichText = !!i2257[9]
  i2256.m_HorizontalOverflow = i2257[10]
  i2256.m_VerticalOverflow = i2257[11]
  i2256.m_LineSpacing = i2257[12]
  return i2256
}

Deserializers["ECS_MagicTile.StarTween"] = function (request, data, root) {
  var i2258 = root || request.c( 'ECS_MagicTile.StarTween' )
  var i2259 = data
  i2258.defaultValue = request.d('ECS_MagicTile.StarTween+StarProperties', i2259[0], i2258.defaultValue)
  return i2258
}

Deserializers["ECS_MagicTile.StarTween+StarProperties"] = function (request, data, root) {
  var i2260 = root || request.c( 'ECS_MagicTile.StarTween+StarProperties' )
  var i2261 = data
  request.r(i2261[0], i2261[1], 0, i2260, 'starRect')
  request.r(i2261[2], i2261[3], 0, i2260, 'starAwakenedImg')
  i2260.scaleStartValue = i2261[4]
  i2260.scaleMidValue = i2261[5]
  i2260.scaleEndValue = i2261[6]
  i2260.rotationStartValue = new pc.Vec3( i2261[7], i2261[8], i2261[9] )
  i2260.rotationMidValue = new pc.Vec3( i2261[10], i2261[11], i2261[12] )
  i2260.rotationEndValue = new pc.Vec3( i2261[13], i2261[14], i2261[15] )
  i2260.awakenedScaleStart = i2261[16]
  i2260.awakenedScaleMid = i2261[17]
  i2260.awakenedScaleEnd = i2261[18]
  i2260.alphaStart = i2261[19]
  i2260.alphaMid = i2261[20]
  i2260.alphaEnd = i2261[21]
  i2260.firstPhaseDuration = i2261[22]
  i2260.firstPhaseEase = i2261[23]
  i2260.secondPhaseDuration = i2261[24]
  i2260.secondPhaseEase = i2261[25]
  return i2260
}

Deserializers["ECS_MagicTile.CrownTween"] = function (request, data, root) {
  var i2262 = root || request.c( 'ECS_MagicTile.CrownTween' )
  var i2263 = data
  i2262.defaultValue = request.d('ECS_MagicTile.CrownTween+CrownProperties', i2263[0], i2262.defaultValue)
  return i2262
}

Deserializers["ECS_MagicTile.CrownTween+CrownProperties"] = function (request, data, root) {
  var i2264 = root || request.c( 'ECS_MagicTile.CrownTween+CrownProperties' )
  var i2265 = data
  request.r(i2265[0], i2265[1], 0, i2264, 'crownRect')
  request.r(i2265[2], i2265[3], 0, i2264, 'crownAwakenedImg')
  i2264.scaleStartValue = i2265[4]
  i2264.scaleMidValue = i2265[5]
  i2264.scaleEndValue = i2265[6]
  i2264.awakenedScaleStart = i2265[7]
  i2264.awakenedScaleMid = i2265[8]
  i2264.awakenedScaleEnd = i2265[9]
  i2264.alphaStart = i2265[10]
  i2264.alphaMid = i2265[11]
  i2264.alphaEnd = i2265[12]
  i2264.firstPhaseDuration = i2265[13]
  i2264.firstPhaseEase = i2265[14]
  i2264.secondPhaseDuration = i2265[15]
  i2264.secondPhaseEase = i2265[16]
  return i2264
}

Deserializers["ECS_MagicTile.ProgressEffectController"] = function (request, data, root) {
  var i2266 = root || request.c( 'ECS_MagicTile.ProgressEffectController' )
  var i2267 = data
  request.r(i2267[0], i2267[1], 0, i2266, 'progressSlider')
  var i2269 = i2267[2]
  var i2268 = []
  for(var i = 0; i < i2269.length; i += 2) {
  request.r(i2269[i + 0], i2269[i + 1], 2, i2268, '')
  }
  i2266.progressPoints = i2268
  var i2271 = i2267[3]
  var i2270 = []
  for(var i = 0; i < i2271.length; i += 1) {
    i2270.push( request.d('ECS_MagicTile.StarTween+StarProperties', i2271[i + 0]) );
  }
  i2266.starPoints = i2270
  var i2273 = i2267[4]
  var i2272 = []
  for(var i = 0; i < i2273.length; i += 1) {
    i2272.push( request.d('ECS_MagicTile.CrownTween+CrownProperties', i2273[i + 0]) );
  }
  i2266.crownPoints = i2272
  return i2266
}

Deserializers["UnityEngine.UI.Slider"] = function (request, data, root) {
  var i2280 = root || request.c( 'UnityEngine.UI.Slider' )
  var i2281 = data
  request.r(i2281[0], i2281[1], 0, i2280, 'm_FillRect')
  request.r(i2281[2], i2281[3], 0, i2280, 'm_HandleRect')
  i2280.m_Direction = i2281[4]
  i2280.m_MinValue = i2281[5]
  i2280.m_MaxValue = i2281[6]
  i2280.m_WholeNumbers = !!i2281[7]
  i2280.m_Value = i2281[8]
  i2280.m_OnValueChanged = request.d('UnityEngine.UI.Slider+SliderEvent', i2281[9], i2280.m_OnValueChanged)
  i2280.m_Navigation = request.d('UnityEngine.UI.Navigation', i2281[10], i2280.m_Navigation)
  i2280.m_Transition = i2281[11]
  i2280.m_Colors = request.d('UnityEngine.UI.ColorBlock', i2281[12], i2280.m_Colors)
  i2280.m_SpriteState = request.d('UnityEngine.UI.SpriteState', i2281[13], i2280.m_SpriteState)
  i2280.m_AnimationTriggers = request.d('UnityEngine.UI.AnimationTriggers', i2281[14], i2280.m_AnimationTriggers)
  i2280.m_Interactable = !!i2281[15]
  request.r(i2281[16], i2281[17], 0, i2280, 'm_TargetGraphic')
  return i2280
}

Deserializers["UnityEngine.UI.Slider+SliderEvent"] = function (request, data, root) {
  var i2282 = root || request.c( 'UnityEngine.UI.Slider+SliderEvent' )
  var i2283 = data
  i2282.m_PersistentCalls = request.d('UnityEngine.Events.PersistentCallGroup', i2283[0], i2282.m_PersistentCalls)
  return i2282
}

Deserializers["UnityEngine.Events.PersistentCallGroup"] = function (request, data, root) {
  var i2284 = root || request.c( 'UnityEngine.Events.PersistentCallGroup' )
  var i2285 = data
  var i2287 = i2285[0]
  var i2286 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Events.PersistentCall')))
  for(var i = 0; i < i2287.length; i += 1) {
    i2286.add(request.d('UnityEngine.Events.PersistentCall', i2287[i + 0]));
  }
  i2284.m_Calls = i2286
  return i2284
}

Deserializers["UnityEngine.Events.PersistentCall"] = function (request, data, root) {
  var i2290 = root || request.c( 'UnityEngine.Events.PersistentCall' )
  var i2291 = data
  request.r(i2291[0], i2291[1], 0, i2290, 'm_Target')
  i2290.m_TargetAssemblyTypeName = i2291[2]
  i2290.m_MethodName = i2291[3]
  i2290.m_Mode = i2291[4]
  i2290.m_Arguments = request.d('UnityEngine.Events.ArgumentCache', i2291[5], i2290.m_Arguments)
  i2290.m_CallState = i2291[6]
  return i2290
}

Deserializers["UnityEngine.UI.Navigation"] = function (request, data, root) {
  var i2292 = root || request.c( 'UnityEngine.UI.Navigation' )
  var i2293 = data
  i2292.m_Mode = i2293[0]
  i2292.m_WrapAround = !!i2293[1]
  request.r(i2293[2], i2293[3], 0, i2292, 'm_SelectOnUp')
  request.r(i2293[4], i2293[5], 0, i2292, 'm_SelectOnDown')
  request.r(i2293[6], i2293[7], 0, i2292, 'm_SelectOnLeft')
  request.r(i2293[8], i2293[9], 0, i2292, 'm_SelectOnRight')
  return i2292
}

Deserializers["UnityEngine.UI.ColorBlock"] = function (request, data, root) {
  var i2294 = root || request.c( 'UnityEngine.UI.ColorBlock' )
  var i2295 = data
  i2294.m_NormalColor = new pc.Color(i2295[0], i2295[1], i2295[2], i2295[3])
  i2294.m_HighlightedColor = new pc.Color(i2295[4], i2295[5], i2295[6], i2295[7])
  i2294.m_PressedColor = new pc.Color(i2295[8], i2295[9], i2295[10], i2295[11])
  i2294.m_SelectedColor = new pc.Color(i2295[12], i2295[13], i2295[14], i2295[15])
  i2294.m_DisabledColor = new pc.Color(i2295[16], i2295[17], i2295[18], i2295[19])
  i2294.m_ColorMultiplier = i2295[20]
  i2294.m_FadeDuration = i2295[21]
  return i2294
}

Deserializers["UnityEngine.UI.SpriteState"] = function (request, data, root) {
  var i2296 = root || request.c( 'UnityEngine.UI.SpriteState' )
  var i2297 = data
  request.r(i2297[0], i2297[1], 0, i2296, 'm_HighlightedSprite')
  request.r(i2297[2], i2297[3], 0, i2296, 'm_PressedSprite')
  request.r(i2297[4], i2297[5], 0, i2296, 'm_SelectedSprite')
  request.r(i2297[6], i2297[7], 0, i2296, 'm_DisabledSprite')
  return i2296
}

Deserializers["UnityEngine.UI.AnimationTriggers"] = function (request, data, root) {
  var i2298 = root || request.c( 'UnityEngine.UI.AnimationTriggers' )
  var i2299 = data
  i2298.m_NormalTrigger = i2299[0]
  i2298.m_HighlightedTrigger = i2299[1]
  i2298.m_PressedTrigger = i2299[2]
  i2298.m_SelectedTrigger = i2299[3]
  i2298.m_DisabledTrigger = i2299[4]
  return i2298
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.ParticleSystem"] = function (request, data, root) {
  var i2300 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.ParticleSystem' )
  var i2301 = data
  i2300.main = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.MainModule', i2301[0], i2300.main)
  i2300.colorBySpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorBySpeedModule', i2301[1], i2300.colorBySpeed)
  i2300.colorOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorOverLifetimeModule', i2301[2], i2300.colorOverLifetime)
  i2300.emission = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.EmissionModule', i2301[3], i2300.emission)
  i2300.rotationBySpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationBySpeedModule', i2301[4], i2300.rotationBySpeed)
  i2300.rotationOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationOverLifetimeModule', i2301[5], i2300.rotationOverLifetime)
  i2300.shape = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ShapeModule', i2301[6], i2300.shape)
  i2300.sizeBySpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeBySpeedModule', i2301[7], i2300.sizeBySpeed)
  i2300.sizeOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeOverLifetimeModule', i2301[8], i2300.sizeOverLifetime)
  i2300.textureSheetAnimation = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.TextureSheetAnimationModule', i2301[9], i2300.textureSheetAnimation)
  i2300.velocityOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.VelocityOverLifetimeModule', i2301[10], i2300.velocityOverLifetime)
  i2300.noise = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.NoiseModule', i2301[11], i2300.noise)
  i2300.inheritVelocity = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.InheritVelocityModule', i2301[12], i2300.inheritVelocity)
  i2300.forceOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ForceOverLifetimeModule', i2301[13], i2300.forceOverLifetime)
  i2300.limitVelocityOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.LimitVelocityOverLifetimeModule', i2301[14], i2300.limitVelocityOverLifetime)
  i2300.useAutoRandomSeed = !!i2301[15]
  i2300.randomSeed = i2301[16]
  return i2300
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.MainModule"] = function (request, data, root) {
  var i2302 = root || new pc.ParticleSystemMain()
  var i2303 = data
  i2302.duration = i2303[0]
  i2302.loop = !!i2303[1]
  i2302.prewarm = !!i2303[2]
  i2302.startDelay = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2303[3], i2302.startDelay)
  i2302.startLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2303[4], i2302.startLifetime)
  i2302.startSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2303[5], i2302.startSpeed)
  i2302.startSize3D = !!i2303[6]
  i2302.startSizeX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2303[7], i2302.startSizeX)
  i2302.startSizeY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2303[8], i2302.startSizeY)
  i2302.startSizeZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2303[9], i2302.startSizeZ)
  i2302.startRotation3D = !!i2303[10]
  i2302.startRotationX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2303[11], i2302.startRotationX)
  i2302.startRotationY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2303[12], i2302.startRotationY)
  i2302.startRotationZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2303[13], i2302.startRotationZ)
  i2302.startColor = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient', i2303[14], i2302.startColor)
  i2302.gravityModifier = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2303[15], i2302.gravityModifier)
  i2302.simulationSpace = i2303[16]
  request.r(i2303[17], i2303[18], 0, i2302, 'customSimulationSpace')
  i2302.simulationSpeed = i2303[19]
  i2302.useUnscaledTime = !!i2303[20]
  i2302.scalingMode = i2303[21]
  i2302.playOnAwake = !!i2303[22]
  i2302.maxParticles = i2303[23]
  i2302.emitterVelocityMode = i2303[24]
  i2302.stopAction = i2303[25]
  return i2302
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve"] = function (request, data, root) {
  var i2304 = root || new pc.MinMaxCurve()
  var i2305 = data
  i2304.mode = i2305[0]
  i2304.curveMin = new pc.AnimationCurve( { keys_flow: i2305[1] } )
  i2304.curveMax = new pc.AnimationCurve( { keys_flow: i2305[2] } )
  i2304.curveMultiplier = i2305[3]
  i2304.constantMin = i2305[4]
  i2304.constantMax = i2305[5]
  return i2304
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient"] = function (request, data, root) {
  var i2306 = root || new pc.MinMaxGradient()
  var i2307 = data
  i2306.mode = i2307[0]
  i2306.gradientMin = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient', i2307[1], i2306.gradientMin)
  i2306.gradientMax = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient', i2307[2], i2306.gradientMax)
  i2306.colorMin = new pc.Color(i2307[3], i2307[4], i2307[5], i2307[6])
  i2306.colorMax = new pc.Color(i2307[7], i2307[8], i2307[9], i2307[10])
  return i2306
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient"] = function (request, data, root) {
  var i2308 = root || request.c( 'Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient' )
  var i2309 = data
  i2308.mode = i2309[0]
  var i2311 = i2309[1]
  var i2310 = []
  for(var i = 0; i < i2311.length; i += 1) {
    i2310.push( request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey', i2311[i + 0]) );
  }
  i2308.colorKeys = i2310
  var i2313 = i2309[2]
  var i2312 = []
  for(var i = 0; i < i2313.length; i += 1) {
    i2312.push( request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey', i2313[i + 0]) );
  }
  i2308.alphaKeys = i2312
  return i2308
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorBySpeedModule"] = function (request, data, root) {
  var i2314 = root || new pc.ParticleSystemColorBySpeed()
  var i2315 = data
  i2314.enabled = !!i2315[0]
  i2314.color = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient', i2315[1], i2314.color)
  i2314.range = new pc.Vec2( i2315[2], i2315[3] )
  return i2314
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey"] = function (request, data, root) {
  var i2318 = root || request.c( 'Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey' )
  var i2319 = data
  i2318.color = new pc.Color(i2319[0], i2319[1], i2319[2], i2319[3])
  i2318.time = i2319[4]
  return i2318
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey"] = function (request, data, root) {
  var i2322 = root || request.c( 'Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey' )
  var i2323 = data
  i2322.alpha = i2323[0]
  i2322.time = i2323[1]
  return i2322
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorOverLifetimeModule"] = function (request, data, root) {
  var i2324 = root || new pc.ParticleSystemColorOverLifetime()
  var i2325 = data
  i2324.enabled = !!i2325[0]
  i2324.color = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient', i2325[1], i2324.color)
  return i2324
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.EmissionModule"] = function (request, data, root) {
  var i2326 = root || new pc.ParticleSystemEmitter()
  var i2327 = data
  i2326.enabled = !!i2327[0]
  i2326.rateOverTime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2327[1], i2326.rateOverTime)
  i2326.rateOverDistance = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2327[2], i2326.rateOverDistance)
  var i2329 = i2327[3]
  var i2328 = []
  for(var i = 0; i < i2329.length; i += 1) {
    i2328.push( request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Burst', i2329[i + 0]) );
  }
  i2326.bursts = i2328
  return i2326
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Burst"] = function (request, data, root) {
  var i2332 = root || new pc.ParticleSystemBurst()
  var i2333 = data
  i2332.count = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2333[0], i2332.count)
  i2332.cycleCount = i2333[1]
  i2332.minCount = i2333[2]
  i2332.maxCount = i2333[3]
  i2332.repeatInterval = i2333[4]
  i2332.time = i2333[5]
  return i2332
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationBySpeedModule"] = function (request, data, root) {
  var i2334 = root || new pc.ParticleSystemRotationBySpeed()
  var i2335 = data
  i2334.enabled = !!i2335[0]
  i2334.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2335[1], i2334.x)
  i2334.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2335[2], i2334.y)
  i2334.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2335[3], i2334.z)
  i2334.separateAxes = !!i2335[4]
  i2334.range = new pc.Vec2( i2335[5], i2335[6] )
  return i2334
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationOverLifetimeModule"] = function (request, data, root) {
  var i2336 = root || new pc.ParticleSystemRotationOverLifetime()
  var i2337 = data
  i2336.enabled = !!i2337[0]
  i2336.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2337[1], i2336.x)
  i2336.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2337[2], i2336.y)
  i2336.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2337[3], i2336.z)
  i2336.separateAxes = !!i2337[4]
  return i2336
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ShapeModule"] = function (request, data, root) {
  var i2338 = root || new pc.ParticleSystemShape()
  var i2339 = data
  i2338.enabled = !!i2339[0]
  i2338.shapeType = i2339[1]
  i2338.randomDirectionAmount = i2339[2]
  i2338.sphericalDirectionAmount = i2339[3]
  i2338.randomPositionAmount = i2339[4]
  i2338.alignToDirection = !!i2339[5]
  i2338.radius = i2339[6]
  i2338.radiusMode = i2339[7]
  i2338.radiusSpread = i2339[8]
  i2338.radiusSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2339[9], i2338.radiusSpeed)
  i2338.radiusThickness = i2339[10]
  i2338.angle = i2339[11]
  i2338.length = i2339[12]
  i2338.boxThickness = new pc.Vec3( i2339[13], i2339[14], i2339[15] )
  i2338.meshShapeType = i2339[16]
  request.r(i2339[17], i2339[18], 0, i2338, 'mesh')
  request.r(i2339[19], i2339[20], 0, i2338, 'meshRenderer')
  request.r(i2339[21], i2339[22], 0, i2338, 'skinnedMeshRenderer')
  i2338.useMeshMaterialIndex = !!i2339[23]
  i2338.meshMaterialIndex = i2339[24]
  i2338.useMeshColors = !!i2339[25]
  i2338.normalOffset = i2339[26]
  i2338.arc = i2339[27]
  i2338.arcMode = i2339[28]
  i2338.arcSpread = i2339[29]
  i2338.arcSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2339[30], i2338.arcSpeed)
  i2338.donutRadius = i2339[31]
  i2338.position = new pc.Vec3( i2339[32], i2339[33], i2339[34] )
  i2338.rotation = new pc.Vec3( i2339[35], i2339[36], i2339[37] )
  i2338.scale = new pc.Vec3( i2339[38], i2339[39], i2339[40] )
  return i2338
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeBySpeedModule"] = function (request, data, root) {
  var i2340 = root || new pc.ParticleSystemSizeBySpeed()
  var i2341 = data
  i2340.enabled = !!i2341[0]
  i2340.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2341[1], i2340.x)
  i2340.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2341[2], i2340.y)
  i2340.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2341[3], i2340.z)
  i2340.separateAxes = !!i2341[4]
  i2340.range = new pc.Vec2( i2341[5], i2341[6] )
  return i2340
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeOverLifetimeModule"] = function (request, data, root) {
  var i2342 = root || new pc.ParticleSystemSizeOverLifetime()
  var i2343 = data
  i2342.enabled = !!i2343[0]
  i2342.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2343[1], i2342.x)
  i2342.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2343[2], i2342.y)
  i2342.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2343[3], i2342.z)
  i2342.separateAxes = !!i2343[4]
  return i2342
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.TextureSheetAnimationModule"] = function (request, data, root) {
  var i2344 = root || new pc.ParticleSystemTextureSheetAnimation()
  var i2345 = data
  i2344.enabled = !!i2345[0]
  i2344.mode = i2345[1]
  i2344.animation = i2345[2]
  i2344.numTilesX = i2345[3]
  i2344.numTilesY = i2345[4]
  i2344.useRandomRow = !!i2345[5]
  i2344.frameOverTime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2345[6], i2344.frameOverTime)
  i2344.startFrame = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2345[7], i2344.startFrame)
  i2344.cycleCount = i2345[8]
  i2344.rowIndex = i2345[9]
  i2344.flipU = i2345[10]
  i2344.flipV = i2345[11]
  i2344.spriteCount = i2345[12]
  var i2347 = i2345[13]
  var i2346 = []
  for(var i = 0; i < i2347.length; i += 2) {
  request.r(i2347[i + 0], i2347[i + 1], 2, i2346, '')
  }
  i2344.sprites = i2346
  return i2344
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.VelocityOverLifetimeModule"] = function (request, data, root) {
  var i2350 = root || new pc.ParticleSystemVelocityOverLifetime()
  var i2351 = data
  i2350.enabled = !!i2351[0]
  i2350.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2351[1], i2350.x)
  i2350.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2351[2], i2350.y)
  i2350.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2351[3], i2350.z)
  i2350.radial = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2351[4], i2350.radial)
  i2350.speedModifier = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2351[5], i2350.speedModifier)
  i2350.space = i2351[6]
  i2350.orbitalX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2351[7], i2350.orbitalX)
  i2350.orbitalY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2351[8], i2350.orbitalY)
  i2350.orbitalZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2351[9], i2350.orbitalZ)
  i2350.orbitalOffsetX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2351[10], i2350.orbitalOffsetX)
  i2350.orbitalOffsetY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2351[11], i2350.orbitalOffsetY)
  i2350.orbitalOffsetZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2351[12], i2350.orbitalOffsetZ)
  return i2350
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.NoiseModule"] = function (request, data, root) {
  var i2352 = root || new pc.ParticleSystemNoise()
  var i2353 = data
  i2352.enabled = !!i2353[0]
  i2352.separateAxes = !!i2353[1]
  i2352.strengthX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2353[2], i2352.strengthX)
  i2352.strengthY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2353[3], i2352.strengthY)
  i2352.strengthZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2353[4], i2352.strengthZ)
  i2352.frequency = i2353[5]
  i2352.damping = !!i2353[6]
  i2352.octaveCount = i2353[7]
  i2352.octaveMultiplier = i2353[8]
  i2352.octaveScale = i2353[9]
  i2352.quality = i2353[10]
  i2352.scrollSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2353[11], i2352.scrollSpeed)
  i2352.scrollSpeedMultiplier = i2353[12]
  i2352.remapEnabled = !!i2353[13]
  i2352.remapX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2353[14], i2352.remapX)
  i2352.remapY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2353[15], i2352.remapY)
  i2352.remapZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2353[16], i2352.remapZ)
  i2352.positionAmount = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2353[17], i2352.positionAmount)
  i2352.rotationAmount = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2353[18], i2352.rotationAmount)
  i2352.sizeAmount = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2353[19], i2352.sizeAmount)
  return i2352
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.InheritVelocityModule"] = function (request, data, root) {
  var i2354 = root || new pc.ParticleSystemInheritVelocity()
  var i2355 = data
  i2354.enabled = !!i2355[0]
  i2354.mode = i2355[1]
  i2354.curve = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2355[2], i2354.curve)
  return i2354
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ForceOverLifetimeModule"] = function (request, data, root) {
  var i2356 = root || new pc.ParticleSystemForceOverLifetime()
  var i2357 = data
  i2356.enabled = !!i2357[0]
  i2356.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2357[1], i2356.x)
  i2356.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2357[2], i2356.y)
  i2356.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2357[3], i2356.z)
  i2356.space = i2357[4]
  i2356.randomized = !!i2357[5]
  return i2356
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.LimitVelocityOverLifetimeModule"] = function (request, data, root) {
  var i2358 = root || new pc.ParticleSystemLimitVelocityOverLifetime()
  var i2359 = data
  i2358.enabled = !!i2359[0]
  i2358.limit = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2359[1], i2358.limit)
  i2358.limitX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2359[2], i2358.limitX)
  i2358.limitY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2359[3], i2358.limitY)
  i2358.limitZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2359[4], i2358.limitZ)
  i2358.dampen = i2359[5]
  i2358.separateAxes = !!i2359[6]
  i2358.space = i2359[7]
  i2358.drag = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i2359[8], i2358.drag)
  i2358.multiplyDragByParticleSize = !!i2359[9]
  i2358.multiplyDragByParticleVelocity = !!i2359[10]
  return i2358
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.ParticleSystemRenderer"] = function (request, data, root) {
  var i2360 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.ParticleSystemRenderer' )
  var i2361 = data
  i2360.enabled = !!i2361[0]
  request.r(i2361[1], i2361[2], 0, i2360, 'sharedMaterial')
  var i2363 = i2361[3]
  var i2362 = []
  for(var i = 0; i < i2363.length; i += 2) {
  request.r(i2363[i + 0], i2363[i + 1], 2, i2362, '')
  }
  i2360.sharedMaterials = i2362
  i2360.receiveShadows = !!i2361[4]
  i2360.shadowCastingMode = i2361[5]
  i2360.sortingLayerID = i2361[6]
  i2360.sortingOrder = i2361[7]
  i2360.lightmapIndex = i2361[8]
  i2360.lightmapSceneIndex = i2361[9]
  i2360.lightmapScaleOffset = new pc.Vec4( i2361[10], i2361[11], i2361[12], i2361[13] )
  i2360.lightProbeUsage = i2361[14]
  i2360.reflectionProbeUsage = i2361[15]
  request.r(i2361[16], i2361[17], 0, i2360, 'mesh')
  i2360.meshCount = i2361[18]
  i2360.activeVertexStreamsCount = i2361[19]
  i2360.alignment = i2361[20]
  i2360.renderMode = i2361[21]
  i2360.sortMode = i2361[22]
  i2360.lengthScale = i2361[23]
  i2360.velocityScale = i2361[24]
  i2360.cameraVelocityScale = i2361[25]
  i2360.normalDirection = i2361[26]
  i2360.sortingFudge = i2361[27]
  i2360.minParticleSize = i2361[28]
  i2360.maxParticleSize = i2361[29]
  i2360.pivot = new pc.Vec3( i2361[30], i2361[31], i2361[32] )
  request.r(i2361[33], i2361[34], 0, i2360, 'trailMaterial')
  return i2360
}

Deserializers["ECS_MagicTile.EffectOnProgress"] = function (request, data, root) {
  var i2364 = root || request.c( 'ECS_MagicTile.EffectOnProgress' )
  var i2365 = data
  i2364.startScale = new pc.Vec2( i2365[0], i2365[1] )
  i2364.endScale = new pc.Vec2( i2365[2], i2365[3] )
  i2364.duration = i2365[4]
  return i2364
}

Deserializers["UnityEngine.EventSystems.EventSystem"] = function (request, data, root) {
  var i2366 = root || request.c( 'UnityEngine.EventSystems.EventSystem' )
  var i2367 = data
  request.r(i2367[0], i2367[1], 0, i2366, 'm_FirstSelected')
  i2366.m_sendNavigationEvents = !!i2367[2]
  i2366.m_DragThreshold = i2367[3]
  return i2366
}

Deserializers["UnityEngine.EventSystems.StandaloneInputModule"] = function (request, data, root) {
  var i2368 = root || request.c( 'UnityEngine.EventSystems.StandaloneInputModule' )
  var i2369 = data
  i2368.m_HorizontalAxis = i2369[0]
  i2368.m_VerticalAxis = i2369[1]
  i2368.m_SubmitButton = i2369[2]
  i2368.m_CancelButton = i2369[3]
  i2368.m_InputActionsPerSecond = i2369[4]
  i2368.m_RepeatDelay = i2369[5]
  i2368.m_ForceModuleActive = !!i2369[6]
  i2368.m_SendPointerHoverToParent = !!i2369[7]
  return i2368
}

Deserializers["ECS_MagicTile.ScreenManager"] = function (request, data, root) {
  var i2370 = root || request.c( 'ECS_MagicTile.ScreenManager' )
  var i2371 = data
  request.r(i2371[0], i2371[1], 0, i2370, 'OnOrientationChange')
  return i2370
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.AudioSource"] = function (request, data, root) {
  var i2372 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.AudioSource' )
  var i2373 = data
  request.r(i2373[0], i2373[1], 0, i2372, 'clip')
  request.r(i2373[2], i2373[3], 0, i2372, 'outputAudioMixerGroup')
  i2372.playOnAwake = !!i2373[4]
  i2372.loop = !!i2373[5]
  i2372.time = i2373[6]
  i2372.volume = i2373[7]
  i2372.pitch = i2373[8]
  i2372.enabled = !!i2373[9]
  return i2372
}

Deserializers["ECS_MagicTile.AudioManager"] = function (request, data, root) {
  var i2374 = root || request.c( 'ECS_MagicTile.AudioManager' )
  var i2375 = data
  request.r(i2375[0], i2375[1], 0, i2374, 'onSongStartChannel')
  request.r(i2375[2], i2375[3], 0, i2374, 'audioClip')
  return i2374
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings"] = function (request, data, root) {
  var i2376 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderSettings' )
  var i2377 = data
  i2376.ambientIntensity = i2377[0]
  i2376.reflectionIntensity = i2377[1]
  i2376.ambientMode = i2377[2]
  i2376.ambientLight = new pc.Color(i2377[3], i2377[4], i2377[5], i2377[6])
  i2376.ambientSkyColor = new pc.Color(i2377[7], i2377[8], i2377[9], i2377[10])
  i2376.ambientGroundColor = new pc.Color(i2377[11], i2377[12], i2377[13], i2377[14])
  i2376.ambientEquatorColor = new pc.Color(i2377[15], i2377[16], i2377[17], i2377[18])
  i2376.fogColor = new pc.Color(i2377[19], i2377[20], i2377[21], i2377[22])
  i2376.fogEndDistance = i2377[23]
  i2376.fogStartDistance = i2377[24]
  i2376.fogDensity = i2377[25]
  i2376.fog = !!i2377[26]
  request.r(i2377[27], i2377[28], 0, i2376, 'skybox')
  i2376.fogMode = i2377[29]
  var i2379 = i2377[30]
  var i2378 = []
  for(var i = 0; i < i2379.length; i += 1) {
    i2378.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap', i2379[i + 0]) );
  }
  i2376.lightmaps = i2378
  i2376.lightProbes = request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes', i2377[31], i2376.lightProbes)
  i2376.lightmapsMode = i2377[32]
  i2376.mixedBakeMode = i2377[33]
  i2376.environmentLightingMode = i2377[34]
  i2376.ambientProbe = new pc.SphericalHarmonicsL2(i2377[35])
  i2376.referenceAmbientProbe = new pc.SphericalHarmonicsL2(i2377[36])
  i2376.useReferenceAmbientProbe = !!i2377[37]
  request.r(i2377[38], i2377[39], 0, i2376, 'customReflection')
  request.r(i2377[40], i2377[41], 0, i2376, 'defaultReflection')
  i2376.defaultReflectionMode = i2377[42]
  i2376.defaultReflectionResolution = i2377[43]
  i2376.sunLightObjectId = i2377[44]
  i2376.pixelLightCount = i2377[45]
  i2376.defaultReflectionHDR = !!i2377[46]
  i2376.hasLightDataAsset = !!i2377[47]
  i2376.hasManualGenerate = !!i2377[48]
  return i2376
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap"] = function (request, data, root) {
  var i2382 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap' )
  var i2383 = data
  request.r(i2383[0], i2383[1], 0, i2382, 'lightmapColor')
  request.r(i2383[2], i2383[3], 0, i2382, 'lightmapDirection')
  return i2382
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes"] = function (request, data, root) {
  var i2384 = root || new UnityEngine.LightProbes()
  var i2385 = data
  return i2384
}

Deserializers["GlobalGameSetting"] = function (request, data, root) {
  var i2392 = root || request.c( 'GlobalGameSetting' )
  var i2393 = data
  request.r(i2393[0], i2393[1], 0, i2392, 'generalSetting')
  request.r(i2393[2], i2393[3], 0, i2392, 'dataSystemSetting')
  request.r(i2393[4], i2393[5], 0, i2392, 'presenterSetting')
  request.r(i2393[6], i2393[7], 0, i2392, 'perfectLineSettingSO')
  request.r(i2393[8], i2393[9], 0, i2392, 'musicNoteSettingSO')
  request.r(i2393[10], i2393[11], 0, i2392, 'notePresenterParent')
  request.r(i2393[12], i2393[13], 0, i2392, 'inputPresenterParent')
  request.r(i2393[14], i2393[15], 0, i2392, 'laneLineSettings')
  request.r(i2393[16], i2393[17], 0, i2392, 'introNoteSetting')
  return i2392
}

Deserializers["GizmoDebugger"] = function (request, data, root) {
  var i2394 = root || request.c( 'GizmoDebugger' )
  var i2395 = data
  i2394.gizmosSize = i2395[0]
  return i2394
}

Deserializers["ManualDebug"] = function (request, data, root) {
  var i2396 = root || request.c( 'ManualDebug' )
  var i2397 = data
  i2396.triggerKey = i2397[0]
  i2396.enableDebugging = !!i2397[1]
  return i2396
}

Deserializers["MusicTileManager"] = function (request, data, root) {
  var i2398 = root || request.c( 'MusicTileManager' )
  var i2399 = data
  return i2398
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader"] = function (request, data, root) {
  var i2400 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader' )
  var i2401 = data
  var i2403 = i2401[0]
  var i2402 = new (System.Collections.Generic.List$1(Bridge.ns('Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError')))
  for(var i = 0; i < i2403.length; i += 1) {
    i2402.add(request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError', i2403[i + 0]));
  }
  i2400.ShaderCompilationErrors = i2402
  i2400.name = i2401[1]
  i2400.guid = i2401[2]
  var i2405 = i2401[3]
  var i2404 = []
  for(var i = 0; i < i2405.length; i += 1) {
    i2404.push( i2405[i + 0] );
  }
  i2400.shaderDefinedKeywords = i2404
  var i2407 = i2401[4]
  var i2406 = []
  for(var i = 0; i < i2407.length; i += 1) {
    i2406.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass', i2407[i + 0]) );
  }
  i2400.passes = i2406
  var i2409 = i2401[5]
  var i2408 = []
  for(var i = 0; i < i2409.length; i += 1) {
    i2408.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass', i2409[i + 0]) );
  }
  i2400.usePasses = i2408
  var i2411 = i2401[6]
  var i2410 = []
  for(var i = 0; i < i2411.length; i += 1) {
    i2410.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue', i2411[i + 0]) );
  }
  i2400.defaultParameterValues = i2410
  request.r(i2401[7], i2401[8], 0, i2400, 'unityFallbackShader')
  i2400.readDepth = !!i2401[9]
  i2400.isCreatedByShaderGraph = !!i2401[10]
  i2400.compiled = !!i2401[11]
  return i2400
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError"] = function (request, data, root) {
  var i2414 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError' )
  var i2415 = data
  i2414.shaderName = i2415[0]
  i2414.errorMessage = i2415[1]
  return i2414
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass"] = function (request, data, root) {
  var i2420 = root || new pc.UnityShaderPass()
  var i2421 = data
  i2420.id = i2421[0]
  i2420.subShaderIndex = i2421[1]
  i2420.name = i2421[2]
  i2420.passType = i2421[3]
  i2420.grabPassTextureName = i2421[4]
  i2420.usePass = !!i2421[5]
  i2420.zTest = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i2421[6], i2420.zTest)
  i2420.zWrite = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i2421[7], i2420.zWrite)
  i2420.culling = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i2421[8], i2420.culling)
  i2420.blending = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending', i2421[9], i2420.blending)
  i2420.alphaBlending = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending', i2421[10], i2420.alphaBlending)
  i2420.colorWriteMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i2421[11], i2420.colorWriteMask)
  i2420.offsetUnits = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i2421[12], i2420.offsetUnits)
  i2420.offsetFactor = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i2421[13], i2420.offsetFactor)
  i2420.stencilRef = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i2421[14], i2420.stencilRef)
  i2420.stencilReadMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i2421[15], i2420.stencilReadMask)
  i2420.stencilWriteMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i2421[16], i2420.stencilWriteMask)
  i2420.stencilOp = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i2421[17], i2420.stencilOp)
  i2420.stencilOpFront = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i2421[18], i2420.stencilOpFront)
  i2420.stencilOpBack = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i2421[19], i2420.stencilOpBack)
  var i2423 = i2421[20]
  var i2422 = []
  for(var i = 0; i < i2423.length; i += 1) {
    i2422.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag', i2423[i + 0]) );
  }
  i2420.tags = i2422
  var i2425 = i2421[21]
  var i2424 = []
  for(var i = 0; i < i2425.length; i += 1) {
    i2424.push( i2425[i + 0] );
  }
  i2420.passDefinedKeywords = i2424
  var i2427 = i2421[22]
  var i2426 = []
  for(var i = 0; i < i2427.length; i += 1) {
    i2426.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup', i2427[i + 0]) );
  }
  i2420.passDefinedKeywordGroups = i2426
  var i2429 = i2421[23]
  var i2428 = []
  for(var i = 0; i < i2429.length; i += 1) {
    i2428.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant', i2429[i + 0]) );
  }
  i2420.variants = i2428
  var i2431 = i2421[24]
  var i2430 = []
  for(var i = 0; i < i2431.length; i += 1) {
    i2430.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant', i2431[i + 0]) );
  }
  i2420.excludedVariants = i2430
  i2420.hasDepthReader = !!i2421[25]
  return i2420
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value"] = function (request, data, root) {
  var i2432 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value' )
  var i2433 = data
  i2432.val = i2433[0]
  i2432.name = i2433[1]
  return i2432
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending"] = function (request, data, root) {
  var i2434 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending' )
  var i2435 = data
  i2434.src = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i2435[0], i2434.src)
  i2434.dst = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i2435[1], i2434.dst)
  i2434.op = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i2435[2], i2434.op)
  return i2434
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp"] = function (request, data, root) {
  var i2436 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp' )
  var i2437 = data
  i2436.pass = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i2437[0], i2436.pass)
  i2436.fail = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i2437[1], i2436.fail)
  i2436.zFail = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i2437[2], i2436.zFail)
  i2436.comp = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i2437[3], i2436.comp)
  return i2436
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag"] = function (request, data, root) {
  var i2440 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag' )
  var i2441 = data
  i2440.name = i2441[0]
  i2440.value = i2441[1]
  return i2440
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup"] = function (request, data, root) {
  var i2444 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup' )
  var i2445 = data
  var i2447 = i2445[0]
  var i2446 = []
  for(var i = 0; i < i2447.length; i += 1) {
    i2446.push( i2447[i + 0] );
  }
  i2444.keywords = i2446
  i2444.hasDiscard = !!i2445[1]
  return i2444
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant"] = function (request, data, root) {
  var i2450 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant' )
  var i2451 = data
  i2450.passId = i2451[0]
  i2450.subShaderIndex = i2451[1]
  var i2453 = i2451[2]
  var i2452 = []
  for(var i = 0; i < i2453.length; i += 1) {
    i2452.push( i2453[i + 0] );
  }
  i2450.keywords = i2452
  i2450.vertexProgram = i2451[3]
  i2450.fragmentProgram = i2451[4]
  i2450.exportedForWebGl2 = !!i2451[5]
  i2450.readDepth = !!i2451[6]
  return i2450
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass"] = function (request, data, root) {
  var i2456 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass' )
  var i2457 = data
  request.r(i2457[0], i2457[1], 0, i2456, 'shader')
  i2456.pass = i2457[2]
  return i2456
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue"] = function (request, data, root) {
  var i2460 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue' )
  var i2461 = data
  i2460.name = i2461[0]
  i2460.type = i2461[1]
  i2460.value = new pc.Vec4( i2461[2], i2461[3], i2461[4], i2461[5] )
  i2460.textureValue = i2461[6]
  i2460.shaderPropertyFlag = i2461[7]
  return i2460
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Sprite"] = function (request, data, root) {
  var i2462 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Sprite' )
  var i2463 = data
  i2462.name = i2463[0]
  request.r(i2463[1], i2463[2], 0, i2462, 'texture')
  i2462.aabb = i2463[3]
  i2462.vertices = i2463[4]
  i2462.triangles = i2463[5]
  i2462.textureRect = UnityEngine.Rect.MinMaxRect(i2463[6], i2463[7], i2463[8], i2463[9])
  i2462.packedRect = UnityEngine.Rect.MinMaxRect(i2463[10], i2463[11], i2463[12], i2463[13])
  i2462.border = new pc.Vec4( i2463[14], i2463[15], i2463[16], i2463[17] )
  i2462.transparency = i2463[18]
  i2462.bounds = i2463[19]
  i2462.pixelsPerUnit = i2463[20]
  i2462.textureWidth = i2463[21]
  i2462.textureHeight = i2463[22]
  i2462.nativeSize = new pc.Vec2( i2463[23], i2463[24] )
  i2462.pivot = new pc.Vec2( i2463[25], i2463[26] )
  i2462.textureRectOffset = new pc.Vec2( i2463[27], i2463[28] )
  return i2462
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.AudioClip"] = function (request, data, root) {
  var i2464 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.AudioClip' )
  var i2465 = data
  i2464.name = i2465[0]
  return i2464
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Font"] = function (request, data, root) {
  var i2466 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Font' )
  var i2467 = data
  i2466.name = i2467[0]
  i2466.ascent = i2467[1]
  i2466.originalLineHeight = i2467[2]
  i2466.fontSize = i2467[3]
  var i2469 = i2467[4]
  var i2468 = []
  for(var i = 0; i < i2469.length; i += 1) {
    i2468.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo', i2469[i + 0]) );
  }
  i2466.characterInfo = i2468
  request.r(i2467[5], i2467[6], 0, i2466, 'texture')
  i2466.originalFontSize = i2467[7]
  return i2466
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo"] = function (request, data, root) {
  var i2472 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo' )
  var i2473 = data
  i2472.index = i2473[0]
  i2472.advance = i2473[1]
  i2472.bearing = i2473[2]
  i2472.glyphWidth = i2473[3]
  i2472.glyphHeight = i2473[4]
  i2472.minX = i2473[5]
  i2472.maxX = i2473[6]
  i2472.minY = i2473[7]
  i2472.maxY = i2473[8]
  i2472.uvBottomLeftX = i2473[9]
  i2472.uvBottomLeftY = i2473[10]
  i2472.uvBottomRightX = i2473[11]
  i2472.uvBottomRightY = i2473[12]
  i2472.uvTopLeftX = i2473[13]
  i2472.uvTopLeftY = i2473[14]
  i2472.uvTopRightX = i2473[15]
  i2472.uvTopRightY = i2473[16]
  return i2472
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.TextAsset"] = function (request, data, root) {
  var i2474 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.TextAsset' )
  var i2475 = data
  i2474.name = i2475[0]
  i2474.bytes64 = i2475[1]
  i2474.data = i2475[2]
  return i2474
}

Deserializers["GeneralGameSetting"] = function (request, data, root) {
  var i2476 = root || request.c( 'GeneralGameSetting' )
  var i2477 = data
  i2476.GameSpeed = i2477[0]
  return i2476
}

Deserializers["ECS_MagicTile.MusicNoteCreationSetting"] = function (request, data, root) {
  var i2478 = root || request.c( 'ECS_MagicTile.MusicNoteCreationSetting' )
  var i2479 = data
  i2478.UsePreciseNoteCalculation = !!i2479[0]
  request.r(i2479[1], i2479[2], 0, i2478, 'MidiContent')
  i2478.ShortNoteScaleYFactor = i2479[3]
  i2478.LongNoteScaleYFactor = i2479[4]
  i2478.startingNoteLane = i2479[5]
  request.r(i2479[6], i2479[7], 0, i2478, 'LongTilePrefab')
  request.r(i2479[8], i2479[9], 0, i2478, 'ShortTilePrefab')
  request.r(i2479[10], i2479[11], 0, i2478, 'startingNotePrefab')
  return i2478
}

Deserializers["PerfectLineSettingSO"] = function (request, data, root) {
  var i2480 = root || request.c( 'PerfectLineSettingSO' )
  var i2481 = data
  i2480.TopLeft = new pc.Vec2( i2481[0], i2481[1] )
  i2480.TopRight = new pc.Vec2( i2481[2], i2481[3] )
  i2480.BottomLeft = new pc.Vec2( i2481[4], i2481[5] )
  i2480.BottomRight = new pc.Vec2( i2481[6], i2481[7] )
  i2480.Position = new pc.Vec2( i2481[8], i2481[9] )
  return i2480
}

Deserializers["ECS_MagicTile.PerfectLineSetting"] = function (request, data, root) {
  var i2482 = root || request.c( 'ECS_MagicTile.PerfectLineSetting' )
  var i2483 = data
  i2482.portraitNormalizedPos = request.d('ECS_MagicTile.PerfectLineSetting+NormalizedFloatPreset', i2483[0], i2482.portraitNormalizedPos)
  i2482.landscapeNormalizedPos = request.d('ECS_MagicTile.PerfectLineSetting+NormalizedFloatPreset', i2483[1], i2482.landscapeNormalizedPos)
  i2482.portraitNormalizedSize = request.d('ECS_MagicTile.PerfectLineSetting+NormalizedFloatPreset', i2483[2], i2482.portraitNormalizedSize)
  i2482.landscapeNormalizedSize = request.d('ECS_MagicTile.PerfectLineSetting+NormalizedFloatPreset', i2483[3], i2482.landscapeNormalizedSize)
  return i2482
}

Deserializers["ECS_MagicTile.LaneLineSettings"] = function (request, data, root) {
  var i2484 = root || request.c( 'ECS_MagicTile.LaneLineSettings' )
  var i2485 = data
  request.r(i2485[0], i2485[1], 0, i2484, 'landLinePrefab')
  i2484.laneLineWidth = request.d('ECS_MagicTile.RangeReactiveFloat', i2485[2], i2484.laneLineWidth)
  return i2484
}

Deserializers["EventChannel.IntEventChannel"] = function (request, data, root) {
  var i2486 = root || request.c( 'EventChannel.IntEventChannel' )
  var i2487 = data
  i2486.maxListeners = i2487[0]
  return i2486
}

Deserializers["EventChannel.BoolEventChannel"] = function (request, data, root) {
  var i2488 = root || request.c( 'EventChannel.BoolEventChannel' )
  var i2489 = data
  i2488.maxListeners = i2489[0]
  return i2488
}

Deserializers["EventChannel.EmptyEventChannel"] = function (request, data, root) {
  var i2490 = root || request.c( 'EventChannel.EmptyEventChannel' )
  var i2491 = data
  i2490.maxListeners = i2491[0]
  return i2490
}

Deserializers["GeneralGameSettingSO"] = function (request, data, root) {
  var i2492 = root || request.c( 'GeneralGameSettingSO' )
  var i2493 = data
  i2492.gameSpeed = i2493[0]
  i2492.baseScaleYForNote = i2493[1]
  request.r(i2493[2], i2493[3], 0, i2492, 'midiContent')
  return i2492
}

Deserializers["DataSystemSettingSO"] = function (request, data, root) {
  var i2494 = root || request.c( 'DataSystemSettingSO' )
  var i2495 = data
  i2494.defaultCapacity = i2495[0]
  return i2494
}

Deserializers["PresenterSettingSO"] = function (request, data, root) {
  var i2496 = root || request.c( 'PresenterSettingSO' )
  var i2497 = data
  request.r(i2497[0], i2497[1], 0, i2496, 'shortMusicNotePresenterPrefab')
  request.r(i2497[2], i2497[3], 0, i2496, 'longMusicNotePresenterPrefab')
  request.r(i2497[4], i2497[5], 0, i2496, 'inputDebuggerPresenterPrefab')
  request.r(i2497[6], i2497[7], 0, i2496, 'laneLinePresenter')
  request.r(i2497[8], i2497[9], 0, i2496, 'introNotePressenyer')
  return i2496
}

Deserializers["MusicNoteSettingSO"] = function (request, data, root) {
  var i2498 = root || request.c( 'MusicNoteSettingSO' )
  var i2499 = data
  i2498.shortNoteScaleYFactor = i2499[0]
  i2498.longNoteScaleYFactor = i2499[1]
  return i2498
}

Deserializers["LaneLineSettingSO"] = function (request, data, root) {
  var i2500 = root || request.c( 'LaneLineSettingSO' )
  var i2501 = data
  i2500.lineWidthPercentage = i2501[0]
  i2500.lineColor = new pc.Color(i2501[1], i2501[2], i2501[3], i2501[4])
  return i2500
}

Deserializers["IntroNoteSettingSO"] = function (request, data, root) {
  var i2502 = root || request.c( 'IntroNoteSettingSO' )
  var i2503 = data
  i2502.introNoteScaleYFactor = i2503[0]
  i2502.initLane = i2503[1]
  return i2502
}

Deserializers["DG.Tweening.Core.DOTweenSettings"] = function (request, data, root) {
  var i2504 = root || request.c( 'DG.Tweening.Core.DOTweenSettings' )
  var i2505 = data
  i2504.useSafeMode = !!i2505[0]
  i2504.safeModeOptions = request.d('DG.Tweening.Core.DOTweenSettings+SafeModeOptions', i2505[1], i2504.safeModeOptions)
  i2504.timeScale = i2505[2]
  i2504.unscaledTimeScale = i2505[3]
  i2504.useSmoothDeltaTime = !!i2505[4]
  i2504.maxSmoothUnscaledTime = i2505[5]
  i2504.rewindCallbackMode = i2505[6]
  i2504.showUnityEditorReport = !!i2505[7]
  i2504.logBehaviour = i2505[8]
  i2504.drawGizmos = !!i2505[9]
  i2504.defaultRecyclable = !!i2505[10]
  i2504.defaultAutoPlay = i2505[11]
  i2504.defaultUpdateType = i2505[12]
  i2504.defaultTimeScaleIndependent = !!i2505[13]
  i2504.defaultEaseType = i2505[14]
  i2504.defaultEaseOvershootOrAmplitude = i2505[15]
  i2504.defaultEasePeriod = i2505[16]
  i2504.defaultAutoKill = !!i2505[17]
  i2504.defaultLoopType = i2505[18]
  i2504.debugMode = !!i2505[19]
  i2504.debugStoreTargetId = !!i2505[20]
  i2504.showPreviewPanel = !!i2505[21]
  i2504.storeSettingsLocation = i2505[22]
  i2504.modules = request.d('DG.Tweening.Core.DOTweenSettings+ModulesSetup', i2505[23], i2504.modules)
  i2504.createASMDEF = !!i2505[24]
  i2504.showPlayingTweens = !!i2505[25]
  i2504.showPausedTweens = !!i2505[26]
  return i2504
}

Deserializers["DG.Tweening.Core.DOTweenSettings+SafeModeOptions"] = function (request, data, root) {
  var i2506 = root || request.c( 'DG.Tweening.Core.DOTweenSettings+SafeModeOptions' )
  var i2507 = data
  i2506.logBehaviour = i2507[0]
  i2506.nestedTweenFailureBehaviour = i2507[1]
  return i2506
}

Deserializers["DG.Tweening.Core.DOTweenSettings+ModulesSetup"] = function (request, data, root) {
  var i2508 = root || request.c( 'DG.Tweening.Core.DOTweenSettings+ModulesSetup' )
  var i2509 = data
  i2508.showPanel = !!i2509[0]
  i2508.audioEnabled = !!i2509[1]
  i2508.physicsEnabled = !!i2509[2]
  i2508.physics2DEnabled = !!i2509[3]
  i2508.spriteEnabled = !!i2509[4]
  i2508.uiEnabled = !!i2509[5]
  i2508.textMeshProEnabled = !!i2509[6]
  i2508.tk2DEnabled = !!i2509[7]
  i2508.deAudioEnabled = !!i2509[8]
  i2508.deUnityExtendedEnabled = !!i2509[9]
  i2508.epoOutlineEnabled = !!i2509[10]
  return i2508
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Resources"] = function (request, data, root) {
  var i2510 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Resources' )
  var i2511 = data
  var i2513 = i2511[0]
  var i2512 = []
  for(var i = 0; i < i2513.length; i += 1) {
    i2512.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Resources+File', i2513[i + 0]) );
  }
  i2510.files = i2512
  i2510.componentToPrefabIds = i2511[1]
  return i2510
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Resources+File"] = function (request, data, root) {
  var i2516 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Resources+File' )
  var i2517 = data
  i2516.path = i2517[0]
  request.r(i2517[1], i2517[2], 0, i2516, 'unityObject')
  return i2516
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings"] = function (request, data, root) {
  var i2518 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings' )
  var i2519 = data
  var i2521 = i2519[0]
  var i2520 = []
  for(var i = 0; i < i2521.length; i += 1) {
    i2520.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder', i2521[i + 0]) );
  }
  i2518.scriptsExecutionOrder = i2520
  var i2523 = i2519[1]
  var i2522 = []
  for(var i = 0; i < i2523.length; i += 1) {
    i2522.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer', i2523[i + 0]) );
  }
  i2518.sortingLayers = i2522
  var i2525 = i2519[2]
  var i2524 = []
  for(var i = 0; i < i2525.length; i += 1) {
    i2524.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer', i2525[i + 0]) );
  }
  i2518.cullingLayers = i2524
  i2518.timeSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings', i2519[3], i2518.timeSettings)
  i2518.physicsSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings', i2519[4], i2518.physicsSettings)
  i2518.physics2DSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings', i2519[5], i2518.physics2DSettings)
  i2518.qualitySettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.QualitySettings', i2519[6], i2518.qualitySettings)
  i2518.enableRealtimeShadows = !!i2519[7]
  i2518.enableAutoInstancing = !!i2519[8]
  i2518.enableDynamicBatching = !!i2519[9]
  i2518.lightmapEncodingQuality = i2519[10]
  i2518.desiredColorSpace = i2519[11]
  var i2527 = i2519[12]
  var i2526 = []
  for(var i = 0; i < i2527.length; i += 1) {
    i2526.push( i2527[i + 0] );
  }
  i2518.allTags = i2526
  return i2518
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder"] = function (request, data, root) {
  var i2530 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder' )
  var i2531 = data
  i2530.name = i2531[0]
  i2530.value = i2531[1]
  return i2530
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer"] = function (request, data, root) {
  var i2534 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer' )
  var i2535 = data
  i2534.id = i2535[0]
  i2534.name = i2535[1]
  i2534.value = i2535[2]
  return i2534
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer"] = function (request, data, root) {
  var i2538 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer' )
  var i2539 = data
  i2538.id = i2539[0]
  i2538.name = i2539[1]
  return i2538
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings"] = function (request, data, root) {
  var i2540 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings' )
  var i2541 = data
  i2540.fixedDeltaTime = i2541[0]
  i2540.maximumDeltaTime = i2541[1]
  i2540.timeScale = i2541[2]
  i2540.maximumParticleTimestep = i2541[3]
  return i2540
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings"] = function (request, data, root) {
  var i2542 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings' )
  var i2543 = data
  i2542.gravity = new pc.Vec3( i2543[0], i2543[1], i2543[2] )
  i2542.defaultSolverIterations = i2543[3]
  i2542.bounceThreshold = i2543[4]
  i2542.autoSyncTransforms = !!i2543[5]
  i2542.autoSimulation = !!i2543[6]
  var i2545 = i2543[7]
  var i2544 = []
  for(var i = 0; i < i2545.length; i += 1) {
    i2544.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask', i2545[i + 0]) );
  }
  i2542.collisionMatrix = i2544
  return i2542
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask"] = function (request, data, root) {
  var i2548 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask' )
  var i2549 = data
  i2548.enabled = !!i2549[0]
  i2548.layerId = i2549[1]
  i2548.otherLayerId = i2549[2]
  return i2548
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings"] = function (request, data, root) {
  var i2550 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings' )
  var i2551 = data
  request.r(i2551[0], i2551[1], 0, i2550, 'material')
  i2550.gravity = new pc.Vec2( i2551[2], i2551[3] )
  i2550.positionIterations = i2551[4]
  i2550.velocityIterations = i2551[5]
  i2550.velocityThreshold = i2551[6]
  i2550.maxLinearCorrection = i2551[7]
  i2550.maxAngularCorrection = i2551[8]
  i2550.maxTranslationSpeed = i2551[9]
  i2550.maxRotationSpeed = i2551[10]
  i2550.baumgarteScale = i2551[11]
  i2550.baumgarteTOIScale = i2551[12]
  i2550.timeToSleep = i2551[13]
  i2550.linearSleepTolerance = i2551[14]
  i2550.angularSleepTolerance = i2551[15]
  i2550.defaultContactOffset = i2551[16]
  i2550.autoSimulation = !!i2551[17]
  i2550.queriesHitTriggers = !!i2551[18]
  i2550.queriesStartInColliders = !!i2551[19]
  i2550.callbacksOnDisable = !!i2551[20]
  i2550.reuseCollisionCallbacks = !!i2551[21]
  i2550.autoSyncTransforms = !!i2551[22]
  var i2553 = i2551[23]
  var i2552 = []
  for(var i = 0; i < i2553.length; i += 1) {
    i2552.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask', i2553[i + 0]) );
  }
  i2550.collisionMatrix = i2552
  return i2550
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask"] = function (request, data, root) {
  var i2556 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask' )
  var i2557 = data
  i2556.enabled = !!i2557[0]
  i2556.layerId = i2557[1]
  i2556.otherLayerId = i2557[2]
  return i2556
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.QualitySettings"] = function (request, data, root) {
  var i2558 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.QualitySettings' )
  var i2559 = data
  var i2561 = i2559[0]
  var i2560 = []
  for(var i = 0; i < i2561.length; i += 1) {
    i2560.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.QualitySettings', i2561[i + 0]) );
  }
  i2558.qualityLevels = i2560
  var i2563 = i2559[1]
  var i2562 = []
  for(var i = 0; i < i2563.length; i += 1) {
    i2562.push( i2563[i + 0] );
  }
  i2558.names = i2562
  i2558.shadows = i2559[2]
  i2558.anisotropicFiltering = i2559[3]
  i2558.antiAliasing = i2559[4]
  i2558.lodBias = i2559[5]
  i2558.shadowCascades = i2559[6]
  i2558.shadowDistance = i2559[7]
  i2558.shadowmaskMode = i2559[8]
  i2558.shadowProjection = i2559[9]
  i2558.shadowResolution = i2559[10]
  i2558.softParticles = !!i2559[11]
  i2558.softVegetation = !!i2559[12]
  i2558.activeColorSpace = i2559[13]
  i2558.desiredColorSpace = i2559[14]
  i2558.masterTextureLimit = i2559[15]
  i2558.maxQueuedFrames = i2559[16]
  i2558.particleRaycastBudget = i2559[17]
  i2558.pixelLightCount = i2559[18]
  i2558.realtimeReflectionProbes = !!i2559[19]
  i2558.shadowCascade2Split = i2559[20]
  i2558.shadowCascade4Split = new pc.Vec3( i2559[21], i2559[22], i2559[23] )
  i2558.streamingMipmapsActive = !!i2559[24]
  i2558.vSyncCount = i2559[25]
  i2558.asyncUploadBufferSize = i2559[26]
  i2558.asyncUploadTimeSlice = i2559[27]
  i2558.billboardsFaceCameraPosition = !!i2559[28]
  i2558.shadowNearPlaneOffset = i2559[29]
  i2558.streamingMipmapsMemoryBudget = i2559[30]
  i2558.maximumLODLevel = i2559[31]
  i2558.streamingMipmapsAddAllCameras = !!i2559[32]
  i2558.streamingMipmapsMaxLevelReduction = i2559[33]
  i2558.streamingMipmapsRenderersPerFrame = i2559[34]
  i2558.resolutionScalingFixedDPIFactor = i2559[35]
  i2558.streamingMipmapsMaxFileIORequests = i2559[36]
  i2558.currentQualityLevel = i2559[37]
  return i2558
}

Deserializers["UnityEngine.Events.ArgumentCache"] = function (request, data, root) {
  var i2566 = root || request.c( 'UnityEngine.Events.ArgumentCache' )
  var i2567 = data
  request.r(i2567[0], i2567[1], 0, i2566, 'm_ObjectArgument')
  i2566.m_ObjectArgumentAssemblyTypeName = i2567[2]
  i2566.m_IntArgument = i2567[3]
  i2566.m_FloatArgument = i2567[4]
  i2566.m_StringArgument = i2567[5]
  i2566.m_BoolArgument = !!i2567[6]
  return i2566
}

Deserializers.fields = {"Luna.Unity.DTO.UnityEngine.Components.Transform":{"position":0,"scale":3,"rotation":6},"Luna.Unity.DTO.UnityEngine.Components.SpriteRenderer":{"enabled":0,"sharedMaterial":1,"sharedMaterials":3,"receiveShadows":4,"shadowCastingMode":5,"sortingLayerID":6,"sortingOrder":7,"lightmapIndex":8,"lightmapSceneIndex":9,"lightmapScaleOffset":10,"lightProbeUsage":14,"reflectionProbeUsage":15,"color":16,"sprite":20,"flipX":22,"flipY":23,"drawMode":24,"size":25,"tileMode":27,"adaptiveModeThreshold":28,"maskInteraction":29,"spriteSortPoint":30},"Luna.Unity.DTO.UnityEngine.Scene.GameObject":{"name":0,"tagId":1,"enabled":2,"isStatic":3,"layer":4},"Luna.Unity.DTO.UnityEngine.Assets.Material":{"name":0,"shader":1,"renderQueue":3,"enableInstancing":4,"floatParameters":5,"colorParameters":6,"vectorParameters":7,"textureParameters":8,"materialFlags":9},"Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag":{"name":0,"enabled":1},"Luna.Unity.DTO.UnityEngine.Components.BoxCollider2D":{"usedByComposite":0,"autoTiling":1,"size":2,"edgeRadius":4,"enabled":5,"isTrigger":6,"usedByEffector":7,"density":8,"offset":9,"material":11},"Luna.Unity.DTO.UnityEngine.Textures.Texture2D":{"name":0,"width":1,"height":2,"mipmapCount":3,"anisoLevel":4,"filterMode":5,"hdr":6,"format":7,"wrapMode":8,"alphaIsTransparency":9,"alphaSource":10,"graphicsFormat":11,"sRGBTexture":12,"desiredColorSpace":13,"wrapU":14,"wrapV":15},"Luna.Unity.DTO.UnityEngine.Components.RectTransform":{"pivot":0,"anchorMin":2,"anchorMax":4,"sizeDelta":6,"anchoredPosition3D":8,"rotation":11,"scale":15},"Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer":{"cullTransparentMesh":0},"Luna.Unity.DTO.UnityEngine.Components.CanvasGroup":{"m_Alpha":0,"m_Interactable":1,"m_BlocksRaycasts":2,"m_IgnoreParentGroups":3,"enabled":4},"Luna.Unity.DTO.UnityEngine.Scene.Scene":{"name":0,"index":1,"startup":2},"Luna.Unity.DTO.UnityEngine.Components.Camera":{"enabled":0,"aspect":1,"orthographic":2,"orthographicSize":3,"backgroundColor":4,"nearClipPlane":8,"farClipPlane":9,"fieldOfView":10,"depth":11,"clearFlags":12,"cullingMask":13,"rect":14,"targetTexture":15,"usePhysicalProperties":17,"focalLength":18,"sensorSize":19,"lensShift":21,"gateFit":23,"commandBufferCount":24,"cameraType":25},"Luna.Unity.DTO.UnityEngine.Components.Canvas":{"enabled":0,"planeDistance":1,"referencePixelsPerUnit":2,"isFallbackOverlay":3,"renderMode":4,"renderOrder":5,"sortingLayerName":6,"sortingOrder":7,"scaleFactor":8,"worldCamera":9,"overrideSorting":11,"pixelPerfect":12,"targetDisplay":13,"overridePixelPerfect":14},"Luna.Unity.DTO.UnityEngine.Components.ParticleSystem":{"main":0,"colorBySpeed":1,"colorOverLifetime":2,"emission":3,"rotationBySpeed":4,"rotationOverLifetime":5,"shape":6,"sizeBySpeed":7,"sizeOverLifetime":8,"textureSheetAnimation":9,"velocityOverLifetime":10,"noise":11,"inheritVelocity":12,"forceOverLifetime":13,"limitVelocityOverLifetime":14,"useAutoRandomSeed":15,"randomSeed":16},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.MainModule":{"duration":0,"loop":1,"prewarm":2,"startDelay":3,"startLifetime":4,"startSpeed":5,"startSize3D":6,"startSizeX":7,"startSizeY":8,"startSizeZ":9,"startRotation3D":10,"startRotationX":11,"startRotationY":12,"startRotationZ":13,"startColor":14,"gravityModifier":15,"simulationSpace":16,"customSimulationSpace":17,"simulationSpeed":19,"useUnscaledTime":20,"scalingMode":21,"playOnAwake":22,"maxParticles":23,"emitterVelocityMode":24,"stopAction":25},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve":{"mode":0,"curveMin":1,"curveMax":2,"curveMultiplier":3,"constantMin":4,"constantMax":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient":{"mode":0,"gradientMin":1,"gradientMax":2,"colorMin":3,"colorMax":7},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient":{"mode":0,"colorKeys":1,"alphaKeys":2},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorBySpeedModule":{"enabled":0,"color":1,"range":2},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey":{"color":0,"time":4},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey":{"alpha":0,"time":1},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorOverLifetimeModule":{"enabled":0,"color":1},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.EmissionModule":{"enabled":0,"rateOverTime":1,"rateOverDistance":2,"bursts":3},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Burst":{"count":0,"cycleCount":1,"minCount":2,"maxCount":3,"repeatInterval":4,"time":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationBySpeedModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4,"range":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ShapeModule":{"enabled":0,"shapeType":1,"randomDirectionAmount":2,"sphericalDirectionAmount":3,"randomPositionAmount":4,"alignToDirection":5,"radius":6,"radiusMode":7,"radiusSpread":8,"radiusSpeed":9,"radiusThickness":10,"angle":11,"length":12,"boxThickness":13,"meshShapeType":16,"mesh":17,"meshRenderer":19,"skinnedMeshRenderer":21,"useMeshMaterialIndex":23,"meshMaterialIndex":24,"useMeshColors":25,"normalOffset":26,"arc":27,"arcMode":28,"arcSpread":29,"arcSpeed":30,"donutRadius":31,"position":32,"rotation":35,"scale":38},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeBySpeedModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4,"range":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.TextureSheetAnimationModule":{"enabled":0,"mode":1,"animation":2,"numTilesX":3,"numTilesY":4,"useRandomRow":5,"frameOverTime":6,"startFrame":7,"cycleCount":8,"rowIndex":9,"flipU":10,"flipV":11,"spriteCount":12,"sprites":13},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.VelocityOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"radial":4,"speedModifier":5,"space":6,"orbitalX":7,"orbitalY":8,"orbitalZ":9,"orbitalOffsetX":10,"orbitalOffsetY":11,"orbitalOffsetZ":12},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.NoiseModule":{"enabled":0,"separateAxes":1,"strengthX":2,"strengthY":3,"strengthZ":4,"frequency":5,"damping":6,"octaveCount":7,"octaveMultiplier":8,"octaveScale":9,"quality":10,"scrollSpeed":11,"scrollSpeedMultiplier":12,"remapEnabled":13,"remapX":14,"remapY":15,"remapZ":16,"positionAmount":17,"rotationAmount":18,"sizeAmount":19},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.InheritVelocityModule":{"enabled":0,"mode":1,"curve":2},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ForceOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"space":4,"randomized":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.LimitVelocityOverLifetimeModule":{"enabled":0,"limit":1,"limitX":2,"limitY":3,"limitZ":4,"dampen":5,"separateAxes":6,"space":7,"drag":8,"multiplyDragByParticleSize":9,"multiplyDragByParticleVelocity":10},"Luna.Unity.DTO.UnityEngine.Components.ParticleSystemRenderer":{"enabled":0,"sharedMaterial":1,"sharedMaterials":3,"receiveShadows":4,"shadowCastingMode":5,"sortingLayerID":6,"sortingOrder":7,"lightmapIndex":8,"lightmapSceneIndex":9,"lightmapScaleOffset":10,"lightProbeUsage":14,"reflectionProbeUsage":15,"mesh":16,"meshCount":18,"activeVertexStreamsCount":19,"alignment":20,"renderMode":21,"sortMode":22,"lengthScale":23,"velocityScale":24,"cameraVelocityScale":25,"normalDirection":26,"sortingFudge":27,"minParticleSize":28,"maxParticleSize":29,"pivot":30,"trailMaterial":33},"Luna.Unity.DTO.UnityEngine.Components.AudioSource":{"clip":0,"outputAudioMixerGroup":2,"playOnAwake":4,"loop":5,"time":6,"volume":7,"pitch":8,"enabled":9},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings":{"ambientIntensity":0,"reflectionIntensity":1,"ambientMode":2,"ambientLight":3,"ambientSkyColor":7,"ambientGroundColor":11,"ambientEquatorColor":15,"fogColor":19,"fogEndDistance":23,"fogStartDistance":24,"fogDensity":25,"fog":26,"skybox":27,"fogMode":29,"lightmaps":30,"lightProbes":31,"lightmapsMode":32,"mixedBakeMode":33,"environmentLightingMode":34,"ambientProbe":35,"referenceAmbientProbe":36,"useReferenceAmbientProbe":37,"customReflection":38,"defaultReflection":40,"defaultReflectionMode":42,"defaultReflectionResolution":43,"sunLightObjectId":44,"pixelLightCount":45,"defaultReflectionHDR":46,"hasLightDataAsset":47,"hasManualGenerate":48},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap":{"lightmapColor":0,"lightmapDirection":2},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes":{"bakedProbes":0,"positions":1,"hullRays":2,"tetrahedra":3,"neighbours":4,"matrices":5},"Luna.Unity.DTO.UnityEngine.Assets.Shader":{"ShaderCompilationErrors":0,"name":1,"guid":2,"shaderDefinedKeywords":3,"passes":4,"usePasses":5,"defaultParameterValues":6,"unityFallbackShader":7,"readDepth":9,"isCreatedByShaderGraph":10,"compiled":11},"Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError":{"shaderName":0,"errorMessage":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass":{"id":0,"subShaderIndex":1,"name":2,"passType":3,"grabPassTextureName":4,"usePass":5,"zTest":6,"zWrite":7,"culling":8,"blending":9,"alphaBlending":10,"colorWriteMask":11,"offsetUnits":12,"offsetFactor":13,"stencilRef":14,"stencilReadMask":15,"stencilWriteMask":16,"stencilOp":17,"stencilOpFront":18,"stencilOpBack":19,"tags":20,"passDefinedKeywords":21,"passDefinedKeywordGroups":22,"variants":23,"excludedVariants":24,"hasDepthReader":25},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value":{"val":0,"name":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending":{"src":0,"dst":1,"op":2},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp":{"pass":0,"fail":1,"zFail":2,"comp":3},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup":{"keywords":0,"hasDiscard":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant":{"passId":0,"subShaderIndex":1,"keywords":2,"vertexProgram":3,"fragmentProgram":4,"exportedForWebGl2":5,"readDepth":6},"Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass":{"shader":0,"pass":2},"Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue":{"name":0,"type":1,"value":2,"textureValue":6,"shaderPropertyFlag":7},"Luna.Unity.DTO.UnityEngine.Textures.Sprite":{"name":0,"texture":1,"aabb":3,"vertices":4,"triangles":5,"textureRect":6,"packedRect":10,"border":14,"transparency":18,"bounds":19,"pixelsPerUnit":20,"textureWidth":21,"textureHeight":22,"nativeSize":23,"pivot":25,"textureRectOffset":27},"Luna.Unity.DTO.UnityEngine.Assets.AudioClip":{"name":0},"Luna.Unity.DTO.UnityEngine.Assets.Font":{"name":0,"ascent":1,"originalLineHeight":2,"fontSize":3,"characterInfo":4,"texture":5,"originalFontSize":7},"Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo":{"index":0,"advance":1,"bearing":2,"glyphWidth":3,"glyphHeight":4,"minX":5,"maxX":6,"minY":7,"maxY":8,"uvBottomLeftX":9,"uvBottomLeftY":10,"uvBottomRightX":11,"uvBottomRightY":12,"uvTopLeftX":13,"uvTopLeftY":14,"uvTopRightX":15,"uvTopRightY":16},"Luna.Unity.DTO.UnityEngine.Assets.TextAsset":{"name":0,"bytes64":1,"data":2},"Luna.Unity.DTO.UnityEngine.Assets.Resources":{"files":0,"componentToPrefabIds":1},"Luna.Unity.DTO.UnityEngine.Assets.Resources+File":{"path":0,"unityObject":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings":{"scriptsExecutionOrder":0,"sortingLayers":1,"cullingLayers":2,"timeSettings":3,"physicsSettings":4,"physics2DSettings":5,"qualitySettings":6,"enableRealtimeShadows":7,"enableAutoInstancing":8,"enableDynamicBatching":9,"lightmapEncodingQuality":10,"desiredColorSpace":11,"allTags":12},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer":{"id":0,"name":1,"value":2},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer":{"id":0,"name":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings":{"fixedDeltaTime":0,"maximumDeltaTime":1,"timeScale":2,"maximumParticleTimestep":3},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings":{"gravity":0,"defaultSolverIterations":3,"bounceThreshold":4,"autoSyncTransforms":5,"autoSimulation":6,"collisionMatrix":7},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask":{"enabled":0,"layerId":1,"otherLayerId":2},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings":{"material":0,"gravity":2,"positionIterations":4,"velocityIterations":5,"velocityThreshold":6,"maxLinearCorrection":7,"maxAngularCorrection":8,"maxTranslationSpeed":9,"maxRotationSpeed":10,"baumgarteScale":11,"baumgarteTOIScale":12,"timeToSleep":13,"linearSleepTolerance":14,"angularSleepTolerance":15,"defaultContactOffset":16,"autoSimulation":17,"queriesHitTriggers":18,"queriesStartInColliders":19,"callbacksOnDisable":20,"reuseCollisionCallbacks":21,"autoSyncTransforms":22,"collisionMatrix":23},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask":{"enabled":0,"layerId":1,"otherLayerId":2},"Luna.Unity.DTO.UnityEngine.Assets.QualitySettings":{"qualityLevels":0,"names":1,"shadows":2,"anisotropicFiltering":3,"antiAliasing":4,"lodBias":5,"shadowCascades":6,"shadowDistance":7,"shadowmaskMode":8,"shadowProjection":9,"shadowResolution":10,"softParticles":11,"softVegetation":12,"activeColorSpace":13,"desiredColorSpace":14,"masterTextureLimit":15,"maxQueuedFrames":16,"particleRaycastBudget":17,"pixelLightCount":18,"realtimeReflectionProbes":19,"shadowCascade2Split":20,"shadowCascade4Split":21,"streamingMipmapsActive":24,"vSyncCount":25,"asyncUploadBufferSize":26,"asyncUploadTimeSlice":27,"billboardsFaceCameraPosition":28,"shadowNearPlaneOffset":29,"streamingMipmapsMemoryBudget":30,"maximumLODLevel":31,"streamingMipmapsAddAllCameras":32,"streamingMipmapsMaxLevelReduction":33,"streamingMipmapsRenderersPerFrame":34,"resolutionScalingFixedDPIFactor":35,"streamingMipmapsMaxFileIORequests":36,"currentQualityLevel":37}}

Deserializers.requiredComponents = {"62":[63],"64":[63],"65":[63],"66":[63],"67":[63],"68":[63],"69":[70],"71":[24],"72":[73],"74":[73],"75":[73],"76":[73],"77":[73],"78":[73],"79":[73],"80":[81],"82":[81],"83":[81],"84":[81],"85":[81],"86":[81],"87":[81],"88":[81],"89":[81],"90":[81],"91":[81],"92":[81],"93":[81],"94":[24],"95":[96],"97":[98],"99":[98],"30":[9],"27":[1],"5":[1],"39":[37,38],"100":[101],"102":[1],"103":[101],"104":[9],"105":[9],"32":[30],"13":[10,9],"106":[9],"31":[30],"107":[9],"108":[9],"109":[9],"110":[9],"111":[9],"112":[9],"113":[9],"114":[9],"115":[9],"116":[10,9],"117":[9],"118":[9],"119":[9],"22":[9],"35":[10,9],"120":[9],"121":[43],"122":[43],"44":[43],"123":[43],"124":[24],"125":[24],"126":[127],"128":[24],"129":[101]}

Deserializers.types = ["UnityEngine.Transform","UnityEngine.SpriteRenderer","UnityEngine.Material","UnityEngine.Sprite","UnityEngine.MonoBehaviour","SpriteCornerDebugger","PerfectLineSettingSO","UnityEngine.Shader","UnityEngine.BoxCollider2D","UnityEngine.RectTransform","UnityEngine.CanvasRenderer","UnityEngine.CanvasGroup","UnityEngine.EventSystems.UIBehaviour","UnityEngine.UI.Image","ECS_MagicTile.GlobalPoint","GeneralGameSetting","ECS_MagicTile.MusicNoteCreationSetting","ECS_MagicTile.PerfectLineSetting","ECS_MagicTile.LaneLineSettings","EventChannel.IntEventChannel","EventChannel.BoolEventChannel","EventChannel.EmptyEventChannel","UnityEngine.UI.Slider","UnityEngine.GameObject","UnityEngine.Camera","UnityEngine.AudioListener","PerfectLineCameraSpacePositionAdjuster","PerfectLineSpriteResizer","PerfectLineFakeVisual","ECS_MagicTile.RaycastToStartGame","UnityEngine.Canvas","UnityEngine.UI.CanvasScaler","UnityEngine.UI.GraphicRaycaster","ECS_MagicTile.ScoreEffectController","BurstMovementUIController","UnityEngine.UI.Text","UnityEngine.Font","ECS_MagicTile.StarTween","ECS_MagicTile.CrownTween","ECS_MagicTile.ProgressEffectController","UnityEngine.ParticleSystem","UnityEngine.ParticleSystemRenderer","ECS_MagicTile.EffectOnProgress","UnityEngine.EventSystems.EventSystem","UnityEngine.EventSystems.StandaloneInputModule","ECS_MagicTile.ScreenManager","UnityEngine.AudioSource","ECS_MagicTile.AudioManager","UnityEngine.AudioClip","GlobalGameSetting","GeneralGameSettingSO","DataSystemSettingSO","PresenterSettingSO","MusicNoteSettingSO","LaneLineSettingSO","IntroNoteSettingSO","GizmoDebugger","ManualDebug","MusicTileManager","UnityEngine.Texture2D","UnityEngine.TextAsset","DG.Tweening.Core.DOTweenSettings","UnityEngine.AudioLowPassFilter","UnityEngine.AudioBehaviour","UnityEngine.AudioHighPassFilter","UnityEngine.AudioReverbFilter","UnityEngine.AudioDistortionFilter","UnityEngine.AudioEchoFilter","UnityEngine.AudioChorusFilter","UnityEngine.Cloth","UnityEngine.SkinnedMeshRenderer","UnityEngine.FlareLayer","UnityEngine.ConstantForce","UnityEngine.Rigidbody","UnityEngine.Joint","UnityEngine.HingeJoint","UnityEngine.SpringJoint","UnityEngine.FixedJoint","UnityEngine.CharacterJoint","UnityEngine.ConfigurableJoint","UnityEngine.CompositeCollider2D","UnityEngine.Rigidbody2D","UnityEngine.Joint2D","UnityEngine.AnchoredJoint2D","UnityEngine.SpringJoint2D","UnityEngine.DistanceJoint2D","UnityEngine.FrictionJoint2D","UnityEngine.HingeJoint2D","UnityEngine.RelativeJoint2D","UnityEngine.SliderJoint2D","UnityEngine.TargetJoint2D","UnityEngine.FixedJoint2D","UnityEngine.WheelJoint2D","UnityEngine.ConstantForce2D","UnityEngine.StreamingController","UnityEngine.TextMesh","UnityEngine.MeshRenderer","UnityEngine.Tilemaps.TilemapRenderer","UnityEngine.Tilemaps.Tilemap","UnityEngine.Tilemaps.TilemapCollider2D","Unity.VisualScripting.SceneVariables","Unity.VisualScripting.Variables","UnityEngine.U2D.Animation.SpriteSkin","Unity.VisualScripting.ScriptMachine","UnityEngine.UI.Dropdown","UnityEngine.UI.Graphic","UnityEngine.UI.AspectRatioFitter","UnityEngine.UI.ContentSizeFitter","UnityEngine.UI.GridLayoutGroup","UnityEngine.UI.HorizontalLayoutGroup","UnityEngine.UI.HorizontalOrVerticalLayoutGroup","UnityEngine.UI.LayoutElement","UnityEngine.UI.LayoutGroup","UnityEngine.UI.VerticalLayoutGroup","UnityEngine.UI.Mask","UnityEngine.UI.MaskableGraphic","UnityEngine.UI.RawImage","UnityEngine.UI.RectMask2D","UnityEngine.UI.Scrollbar","UnityEngine.UI.ScrollRect","UnityEngine.UI.Toggle","UnityEngine.EventSystems.BaseInputModule","UnityEngine.EventSystems.PointerInputModule","UnityEngine.EventSystems.TouchInputModule","UnityEngine.EventSystems.Physics2DRaycaster","UnityEngine.EventSystems.PhysicsRaycaster","UnityEngine.U2D.SpriteShapeController","UnityEngine.U2D.SpriteShapeRenderer","UnityEngine.U2D.PixelPerfectCamera","Unity.VisualScripting.StateMachine"]

Deserializers.unityVersion = "2022.3.58f1";

Deserializers.productName = "DOD-Project";

Deserializers.lunaInitializationTime = "02/20/2025 09:40:26";

Deserializers.lunaDaysRunning = "0.0";

Deserializers.lunaVersion = "6.2.1";

Deserializers.lunaSHA = "28f227c1b455c28500de29df936f0d1376ee9c43";

Deserializers.creativeName = "";

Deserializers.lunaAppID = "24139";

Deserializers.projectId = "28ff3cc24df224f1a92a453c87a1b52f";

Deserializers.packagesInfo = "com.unity.timeline: 1.8.7";

Deserializers.externalJsLibraries = "";

Deserializers.androidLink = ( typeof window !== "undefined")&&window.$environment.packageConfig.androidLink?window.$environment.packageConfig.androidLink:'Empty';

Deserializers.iosLink = ( typeof window !== "undefined")&&window.$environment.packageConfig.iosLink?window.$environment.packageConfig.iosLink:'Empty';

Deserializers.base64Enabled = "False";

Deserializers.minifyEnabled = "True";

Deserializers.isForceUncompressed = "False";

Deserializers.isAntiAliasingEnabled = "False";

Deserializers.isRuntimeAnalysisEnabledForCode = "True";

Deserializers.runtimeAnalysisExcludedClassesCount = "0";

Deserializers.runtimeAnalysisExcludedMethodsCount = "0";

Deserializers.runtimeAnalysisExcludedModules = "";

Deserializers.isRuntimeAnalysisEnabledForShaders = "True";

Deserializers.isRealtimeShadowsEnabled = "False";

Deserializers.isReferenceAmbientProbeBaked = "False";

Deserializers.isLunaCompilerV2Used = "False";

Deserializers.companyName = "DefaultCompany";

Deserializers.buildPlatform = "StandaloneOSX";

Deserializers.applicationIdentifier = "com.DefaultCompany.2DProject";

Deserializers.disableAntiAliasing = true;

Deserializers.graphicsConstraint = 28;

Deserializers.linearColorSpace = false;

Deserializers.buildID = "d123405d-1299-4e40-bf32-5623e7cbe414";

Deserializers.runtimeInitializeOnLoadInfos = [[["UnityEngine","Experimental","Rendering","ScriptableRuntimeReflectionSystemSettings","ScriptingDirtyReflectionSystemInstance"]],[["Unity","VisualScripting","RuntimeVSUsageUtility","RuntimeInitializeOnLoadBeforeSceneLoad"],["PrimeTween","PrimeTweenManager","beforeSceneLoad"]],[["$BurstDirectCallInitializer","Initialize"],["$BurstDirectCallInitializer","Initialize"],["$BurstDirectCallInitializer","Initialize"],["$BurstDirectCallInitializer","Initialize"],["$BurstDirectCallInitializer","Initialize"],["$BurstDirectCallInitializer","Initialize"],["$BurstDirectCallInitializer","Initialize"]],[],[]];

Deserializers.typeNameToIdMap = function(){ var i = 0; return Deserializers.types.reduce( function( res, item ) { res[ item ] = i++; return res; }, {} ) }()

