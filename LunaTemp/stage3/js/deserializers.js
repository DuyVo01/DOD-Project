var Deserializers = {}
Deserializers["UnityEngine.JointSpring"] = function (request, data, root) {
  var i436 = root || request.c( 'UnityEngine.JointSpring' )
  var i437 = data
  i436.spring = i437[0]
  i436.damper = i437[1]
  i436.targetPosition = i437[2]
  return i436
}

Deserializers["UnityEngine.JointMotor"] = function (request, data, root) {
  var i438 = root || request.c( 'UnityEngine.JointMotor' )
  var i439 = data
  i438.m_TargetVelocity = i439[0]
  i438.m_Force = i439[1]
  i438.m_FreeSpin = i439[2]
  return i438
}

Deserializers["UnityEngine.JointLimits"] = function (request, data, root) {
  var i440 = root || request.c( 'UnityEngine.JointLimits' )
  var i441 = data
  i440.m_Min = i441[0]
  i440.m_Max = i441[1]
  i440.m_Bounciness = i441[2]
  i440.m_BounceMinVelocity = i441[3]
  i440.m_ContactDistance = i441[4]
  i440.minBounce = i441[5]
  i440.maxBounce = i441[6]
  return i440
}

Deserializers["UnityEngine.JointDrive"] = function (request, data, root) {
  var i442 = root || request.c( 'UnityEngine.JointDrive' )
  var i443 = data
  i442.m_PositionSpring = i443[0]
  i442.m_PositionDamper = i443[1]
  i442.m_MaximumForce = i443[2]
  i442.m_UseAcceleration = i443[3]
  return i442
}

Deserializers["UnityEngine.SoftJointLimitSpring"] = function (request, data, root) {
  var i444 = root || request.c( 'UnityEngine.SoftJointLimitSpring' )
  var i445 = data
  i444.m_Spring = i445[0]
  i444.m_Damper = i445[1]
  return i444
}

Deserializers["UnityEngine.SoftJointLimit"] = function (request, data, root) {
  var i446 = root || request.c( 'UnityEngine.SoftJointLimit' )
  var i447 = data
  i446.m_Limit = i447[0]
  i446.m_Bounciness = i447[1]
  i446.m_ContactDistance = i447[2]
  return i446
}

Deserializers["UnityEngine.WheelFrictionCurve"] = function (request, data, root) {
  var i448 = root || request.c( 'UnityEngine.WheelFrictionCurve' )
  var i449 = data
  i448.m_ExtremumSlip = i449[0]
  i448.m_ExtremumValue = i449[1]
  i448.m_AsymptoteSlip = i449[2]
  i448.m_AsymptoteValue = i449[3]
  i448.m_Stiffness = i449[4]
  return i448
}

Deserializers["UnityEngine.JointAngleLimits2D"] = function (request, data, root) {
  var i450 = root || request.c( 'UnityEngine.JointAngleLimits2D' )
  var i451 = data
  i450.m_LowerAngle = i451[0]
  i450.m_UpperAngle = i451[1]
  return i450
}

Deserializers["UnityEngine.JointMotor2D"] = function (request, data, root) {
  var i452 = root || request.c( 'UnityEngine.JointMotor2D' )
  var i453 = data
  i452.m_MotorSpeed = i453[0]
  i452.m_MaximumMotorTorque = i453[1]
  return i452
}

Deserializers["UnityEngine.JointSuspension2D"] = function (request, data, root) {
  var i454 = root || request.c( 'UnityEngine.JointSuspension2D' )
  var i455 = data
  i454.m_DampingRatio = i455[0]
  i454.m_Frequency = i455[1]
  i454.m_Angle = i455[2]
  return i454
}

Deserializers["UnityEngine.JointTranslationLimits2D"] = function (request, data, root) {
  var i456 = root || request.c( 'UnityEngine.JointTranslationLimits2D' )
  var i457 = data
  i456.m_LowerTranslation = i457[0]
  i456.m_UpperTranslation = i457[1]
  return i456
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Transform"] = function (request, data, root) {
  var i458 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Transform' )
  var i459 = data
  i458.position = new pc.Vec3( i459[0], i459[1], i459[2] )
  i458.scale = new pc.Vec3( i459[3], i459[4], i459[5] )
  i458.rotation = new pc.Quat(i459[6], i459[7], i459[8], i459[9])
  return i458
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.SpriteRenderer"] = function (request, data, root) {
  var i460 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.SpriteRenderer' )
  var i461 = data
  i460.enabled = !!i461[0]
  request.r(i461[1], i461[2], 0, i460, 'sharedMaterial')
  var i463 = i461[3]
  var i462 = []
  for(var i = 0; i < i463.length; i += 2) {
  request.r(i463[i + 0], i463[i + 1], 2, i462, '')
  }
  i460.sharedMaterials = i462
  i460.receiveShadows = !!i461[4]
  i460.shadowCastingMode = i461[5]
  i460.sortingLayerID = i461[6]
  i460.sortingOrder = i461[7]
  i460.lightmapIndex = i461[8]
  i460.lightmapSceneIndex = i461[9]
  i460.lightmapScaleOffset = new pc.Vec4( i461[10], i461[11], i461[12], i461[13] )
  i460.lightProbeUsage = i461[14]
  i460.reflectionProbeUsage = i461[15]
  i460.color = new pc.Color(i461[16], i461[17], i461[18], i461[19])
  request.r(i461[20], i461[21], 0, i460, 'sprite')
  i460.flipX = !!i461[22]
  i460.flipY = !!i461[23]
  i460.drawMode = i461[24]
  i460.size = new pc.Vec2( i461[25], i461[26] )
  i460.tileMode = i461[27]
  i460.adaptiveModeThreshold = i461[28]
  i460.maskInteraction = i461[29]
  i460.spriteSortPoint = i461[30]
  return i460
}

Deserializers["SpriteCornerDebugger"] = function (request, data, root) {
  var i466 = root || request.c( 'SpriteCornerDebugger' )
  var i467 = data
  i466.gizmoColor = new pc.Color(i467[0], i467[1], i467[2], i467[3])
  i466.gizmoSize = i467[4]
  request.r(i467[5], i467[6], 0, i466, 'generalGameSettingSO')
  return i466
}

Deserializers["Luna.Unity.DTO.UnityEngine.Scene.GameObject"] = function (request, data, root) {
  var i468 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Scene.GameObject' )
  var i469 = data
  i468.name = i469[0]
  i468.tagId = i469[1]
  i468.enabled = !!i469[2]
  i468.isStatic = !!i469[3]
  i468.layer = i469[4]
  return i468
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material"] = function (request, data, root) {
  var i470 = root || new pc.UnityMaterial()
  var i471 = data
  i470.name = i471[0]
  request.r(i471[1], i471[2], 0, i470, 'shader')
  i470.renderQueue = i471[3]
  i470.enableInstancing = !!i471[4]
  var i473 = i471[5]
  var i472 = []
  for(var i = 0; i < i473.length; i += 1) {
    i472.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter', i473[i + 0]) );
  }
  i470.floatParameters = i472
  var i475 = i471[6]
  var i474 = []
  for(var i = 0; i < i475.length; i += 1) {
    i474.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter', i475[i + 0]) );
  }
  i470.colorParameters = i474
  var i477 = i471[7]
  var i476 = []
  for(var i = 0; i < i477.length; i += 1) {
    i476.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter', i477[i + 0]) );
  }
  i470.vectorParameters = i476
  var i479 = i471[8]
  var i478 = []
  for(var i = 0; i < i479.length; i += 1) {
    i478.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter', i479[i + 0]) );
  }
  i470.textureParameters = i478
  var i481 = i471[9]
  var i480 = []
  for(var i = 0; i < i481.length; i += 1) {
    i480.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag', i481[i + 0]) );
  }
  i470.materialFlags = i480
  return i470
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter"] = function (request, data, root) {
  var i484 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter' )
  var i485 = data
  i484.name = i485[0]
  i484.value = i485[1]
  return i484
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter"] = function (request, data, root) {
  var i488 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter' )
  var i489 = data
  i488.name = i489[0]
  i488.value = new pc.Color(i489[1], i489[2], i489[3], i489[4])
  return i488
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter"] = function (request, data, root) {
  var i492 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter' )
  var i493 = data
  i492.name = i493[0]
  i492.value = new pc.Vec4( i493[1], i493[2], i493[3], i493[4] )
  return i492
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter"] = function (request, data, root) {
  var i496 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter' )
  var i497 = data
  i496.name = i497[0]
  request.r(i497[1], i497[2], 0, i496, 'value')
  return i496
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag"] = function (request, data, root) {
  var i500 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag' )
  var i501 = data
  i500.name = i501[0]
  i500.enabled = !!i501[1]
  return i500
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.BoxCollider2D"] = function (request, data, root) {
  var i502 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.BoxCollider2D' )
  var i503 = data
  i502.usedByComposite = !!i503[0]
  i502.autoTiling = !!i503[1]
  i502.size = new pc.Vec2( i503[2], i503[3] )
  i502.edgeRadius = i503[4]
  i502.enabled = !!i503[5]
  i502.isTrigger = !!i503[6]
  i502.usedByEffector = !!i503[7]
  i502.density = i503[8]
  i502.offset = new pc.Vec2( i503[9], i503[10] )
  request.r(i503[11], i503[12], 0, i502, 'material')
  return i502
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Texture2D"] = function (request, data, root) {
  var i504 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Texture2D' )
  var i505 = data
  i504.name = i505[0]
  i504.width = i505[1]
  i504.height = i505[2]
  i504.mipmapCount = i505[3]
  i504.anisoLevel = i505[4]
  i504.filterMode = i505[5]
  i504.hdr = !!i505[6]
  i504.format = i505[7]
  i504.wrapMode = i505[8]
  i504.alphaIsTransparency = !!i505[9]
  i504.alphaSource = i505[10]
  i504.graphicsFormat = i505[11]
  i504.sRGBTexture = !!i505[12]
  i504.desiredColorSpace = i505[13]
  i504.wrapU = i505[14]
  i504.wrapV = i505[15]
  return i504
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.RectTransform"] = function (request, data, root) {
  var i506 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.RectTransform' )
  var i507 = data
  i506.pivot = new pc.Vec2( i507[0], i507[1] )
  i506.anchorMin = new pc.Vec2( i507[2], i507[3] )
  i506.anchorMax = new pc.Vec2( i507[4], i507[5] )
  i506.sizeDelta = new pc.Vec2( i507[6], i507[7] )
  i506.anchoredPosition3D = new pc.Vec3( i507[8], i507[9], i507[10] )
  i506.rotation = new pc.Quat(i507[11], i507[12], i507[13], i507[14])
  i506.scale = new pc.Vec3( i507[15], i507[16], i507[17] )
  return i506
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer"] = function (request, data, root) {
  var i508 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer' )
  var i509 = data
  i508.cullTransparentMesh = !!i509[0]
  return i508
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.CanvasGroup"] = function (request, data, root) {
  var i510 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.CanvasGroup' )
  var i511 = data
  i510.m_Alpha = i511[0]
  i510.m_Interactable = !!i511[1]
  i510.m_BlocksRaycasts = !!i511[2]
  i510.m_IgnoreParentGroups = !!i511[3]
  i510.enabled = !!i511[4]
  return i510
}

Deserializers["UnityEngine.UI.Image"] = function (request, data, root) {
  var i512 = root || request.c( 'UnityEngine.UI.Image' )
  var i513 = data
  request.r(i513[0], i513[1], 0, i512, 'm_Sprite')
  i512.m_Type = i513[2]
  i512.m_PreserveAspect = !!i513[3]
  i512.m_FillCenter = !!i513[4]
  i512.m_FillMethod = i513[5]
  i512.m_FillAmount = i513[6]
  i512.m_FillClockwise = !!i513[7]
  i512.m_FillOrigin = i513[8]
  i512.m_UseSpriteMesh = !!i513[9]
  i512.m_PixelsPerUnitMultiplier = i513[10]
  request.r(i513[11], i513[12], 0, i512, 'm_Material')
  i512.m_Maskable = !!i513[13]
  i512.m_Color = new pc.Color(i513[14], i513[15], i513[16], i513[17])
  i512.m_RaycastTarget = !!i513[18]
  i512.m_RaycastPadding = new pc.Vec4( i513[19], i513[20], i513[21], i513[22] )
  return i512
}

Deserializers["Luna.Unity.DTO.UnityEngine.Scene.Scene"] = function (request, data, root) {
  var i514 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Scene.Scene' )
  var i515 = data
  i514.name = i515[0]
  i514.index = i515[1]
  i514.startup = !!i515[2]
  return i514
}

Deserializers["ECS_MagicTile.GlobalPoint"] = function (request, data, root) {
  var i516 = root || request.c( 'ECS_MagicTile.GlobalPoint' )
  var i517 = data
  request.r(i517[0], i517[1], 0, i516, 'generalGameSetting')
  request.r(i517[2], i517[3], 0, i516, 'musicNoteCreationSettings')
  request.r(i517[4], i517[5], 0, i516, 'perfectLineSetting')
  request.r(i517[6], i517[7], 0, i516, 'laneLineSettings')
  request.r(i517[8], i517[9], 0, i516, 'OnGameStartChannel')
  request.r(i517[10], i517[11], 0, i516, 'OnScoreHitChannel')
  request.r(i517[12], i517[13], 0, i516, 'OnOrientationChangedChannel')
  request.r(i517[14], i517[15], 0, i516, 'OnSongStartChannel')
  request.r(i517[16], i517[17], 0, i516, 'scoreText')
  request.r(i517[18], i517[19], 0, i516, 'progressSlider')
  request.r(i517[20], i517[21], 0, i516, 'perfectLineObject')
  request.r(i517[22], i517[23], 0, i516, 'mainCamera')
  request.r(i517[24], i517[25], 0, i516, 'gameIntroSystem')
  return i516
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Camera"] = function (request, data, root) {
  var i518 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Camera' )
  var i519 = data
  i518.enabled = !!i519[0]
  i518.aspect = i519[1]
  i518.orthographic = !!i519[2]
  i518.orthographicSize = i519[3]
  i518.backgroundColor = new pc.Color(i519[4], i519[5], i519[6], i519[7])
  i518.nearClipPlane = i519[8]
  i518.farClipPlane = i519[9]
  i518.fieldOfView = i519[10]
  i518.depth = i519[11]
  i518.clearFlags = i519[12]
  i518.cullingMask = i519[13]
  i518.rect = i519[14]
  request.r(i519[15], i519[16], 0, i518, 'targetTexture')
  i518.usePhysicalProperties = !!i519[17]
  i518.focalLength = i519[18]
  i518.sensorSize = new pc.Vec2( i519[19], i519[20] )
  i518.lensShift = new pc.Vec2( i519[21], i519[22] )
  i518.gateFit = i519[23]
  i518.commandBufferCount = i519[24]
  i518.cameraType = i519[25]
  return i518
}

Deserializers["PerfectLineCameraSpacePositionAdjuster"] = function (request, data, root) {
  var i520 = root || request.c( 'PerfectLineCameraSpacePositionAdjuster' )
  var i521 = data
  request.r(i521[0], i521[1], 0, i520, 'targetCamera')
  request.r(i521[2], i521[3], 0, i520, 'perfectLineSetting')
  i520.portraitNormalizedPos = request.d('ECS_MagicTile.PerfectLineSetting+NormalizedFloatPreset', i521[4], i520.portraitNormalizedPos)
  i520.landscapeNormalizedPos = request.d('ECS_MagicTile.PerfectLineSetting+NormalizedFloatPreset', i521[5], i520.landscapeNormalizedPos)
  return i520
}

Deserializers["ECS_MagicTile.PerfectLineSetting+NormalizedFloatPreset"] = function (request, data, root) {
  var i522 = root || request.c( 'ECS_MagicTile.PerfectLineSetting+NormalizedFloatPreset' )
  var i523 = data
  i522.normalizedX = request.d('ECS_MagicTile.RangeReactiveFloat', i523[0], i522.normalizedX)
  i522.normalizedY = request.d('ECS_MagicTile.RangeReactiveFloat', i523[1], i522.normalizedY)
  return i522
}

Deserializers["ECS_MagicTile.RangeReactiveFloat"] = function (request, data, root) {
  var i524 = root || request.c( 'ECS_MagicTile.RangeReactiveFloat' )
  var i525 = data
  i524._value = i525[0]
  return i524
}

Deserializers["PerfectLineSpriteResizer"] = function (request, data, root) {
  var i526 = root || request.c( 'PerfectLineSpriteResizer' )
  var i527 = data
  request.r(i527[0], i527[1], 0, i526, 'targetCamera')
  request.r(i527[2], i527[3], 0, i526, 'perfectLineSetting')
  i526.portraitNormalizedSize = request.d('ECS_MagicTile.PerfectLineSetting+NormalizedFloatPreset', i527[4], i526.portraitNormalizedSize)
  i526.landscapeNormalizedSize = request.d('ECS_MagicTile.PerfectLineSetting+NormalizedFloatPreset', i527[5], i526.landscapeNormalizedSize)
  i526.maintainAspectRatio = !!i527[6]
  return i526
}

Deserializers["PerfectLineFakeVisual"] = function (request, data, root) {
  var i528 = root || request.c( 'PerfectLineFakeVisual' )
  var i529 = data
  request.r(i529[0], i529[1], 0, i528, 'perfectLineSetting')
  request.r(i529[2], i529[3], 0, i528, 'targetCamera')
  request.r(i529[4], i529[5], 0, i528, 'onOrientationChangedChannel')
  return i528
}

Deserializers["ECS_MagicTile.RaycastToStartGame"] = function (request, data, root) {
  var i530 = root || request.c( 'ECS_MagicTile.RaycastToStartGame' )
  var i531 = data
  i530.targetLayer = UnityEngine.LayerMask.FromIntegerValue( i531[0] )
  request.r(i531[1], i531[2], 0, i530, 'OnGameStartChannel')
  return i530
}

Deserializers["ECS_MagicTile.GameIntroSystem"] = function (request, data, root) {
  var i532 = root || request.c( 'ECS_MagicTile.GameIntroSystem' )
  var i533 = data
  request.r(i533[0], i533[1], 0, i532, 'generalGameSetting')
  request.r(i533[2], i533[3], 0, i532, 'startButton')
  return i532
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Canvas"] = function (request, data, root) {
  var i534 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Canvas' )
  var i535 = data
  i534.enabled = !!i535[0]
  i534.planeDistance = i535[1]
  i534.referencePixelsPerUnit = i535[2]
  i534.isFallbackOverlay = !!i535[3]
  i534.renderMode = i535[4]
  i534.renderOrder = i535[5]
  i534.sortingLayerName = i535[6]
  i534.sortingOrder = i535[7]
  i534.scaleFactor = i535[8]
  request.r(i535[9], i535[10], 0, i534, 'worldCamera')
  i534.overrideSorting = !!i535[11]
  i534.pixelPerfect = !!i535[12]
  i534.targetDisplay = i535[13]
  i534.overridePixelPerfect = !!i535[14]
  return i534
}

Deserializers["UnityEngine.UI.CanvasScaler"] = function (request, data, root) {
  var i536 = root || request.c( 'UnityEngine.UI.CanvasScaler' )
  var i537 = data
  i536.m_UiScaleMode = i537[0]
  i536.m_ReferencePixelsPerUnit = i537[1]
  i536.m_ScaleFactor = i537[2]
  i536.m_ReferenceResolution = new pc.Vec2( i537[3], i537[4] )
  i536.m_ScreenMatchMode = i537[5]
  i536.m_MatchWidthOrHeight = i537[6]
  i536.m_PhysicalUnit = i537[7]
  i536.m_FallbackScreenDPI = i537[8]
  i536.m_DefaultSpriteDPI = i537[9]
  i536.m_DynamicPixelsPerUnit = i537[10]
  i536.m_PresetInfoIsWorld = !!i537[11]
  return i536
}

Deserializers["UnityEngine.UI.GraphicRaycaster"] = function (request, data, root) {
  var i538 = root || request.c( 'UnityEngine.UI.GraphicRaycaster' )
  var i539 = data
  i538.m_IgnoreReversedGraphics = !!i539[0]
  i538.m_BlockingObjects = i539[1]
  i538.m_BlockingMask = UnityEngine.LayerMask.FromIntegerValue( i539[2] )
  return i538
}

Deserializers["UnityEngine.UI.Button"] = function (request, data, root) {
  var i540 = root || request.c( 'UnityEngine.UI.Button' )
  var i541 = data
  i540.m_OnClick = request.d('UnityEngine.UI.Button+ButtonClickedEvent', i541[0], i540.m_OnClick)
  i540.m_Navigation = request.d('UnityEngine.UI.Navigation', i541[1], i540.m_Navigation)
  i540.m_Transition = i541[2]
  i540.m_Colors = request.d('UnityEngine.UI.ColorBlock', i541[3], i540.m_Colors)
  i540.m_SpriteState = request.d('UnityEngine.UI.SpriteState', i541[4], i540.m_SpriteState)
  i540.m_AnimationTriggers = request.d('UnityEngine.UI.AnimationTriggers', i541[5], i540.m_AnimationTriggers)
  i540.m_Interactable = !!i541[6]
  request.r(i541[7], i541[8], 0, i540, 'm_TargetGraphic')
  return i540
}

Deserializers["UnityEngine.UI.Button+ButtonClickedEvent"] = function (request, data, root) {
  var i542 = root || request.c( 'UnityEngine.UI.Button+ButtonClickedEvent' )
  var i543 = data
  i542.m_PersistentCalls = request.d('UnityEngine.Events.PersistentCallGroup', i543[0], i542.m_PersistentCalls)
  return i542
}

Deserializers["UnityEngine.Events.PersistentCallGroup"] = function (request, data, root) {
  var i544 = root || request.c( 'UnityEngine.Events.PersistentCallGroup' )
  var i545 = data
  var i547 = i545[0]
  var i546 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Events.PersistentCall')))
  for(var i = 0; i < i547.length; i += 1) {
    i546.add(request.d('UnityEngine.Events.PersistentCall', i547[i + 0]));
  }
  i544.m_Calls = i546
  return i544
}

Deserializers["UnityEngine.Events.PersistentCall"] = function (request, data, root) {
  var i550 = root || request.c( 'UnityEngine.Events.PersistentCall' )
  var i551 = data
  request.r(i551[0], i551[1], 0, i550, 'm_Target')
  i550.m_TargetAssemblyTypeName = i551[2]
  i550.m_MethodName = i551[3]
  i550.m_Mode = i551[4]
  i550.m_Arguments = request.d('UnityEngine.Events.ArgumentCache', i551[5], i550.m_Arguments)
  i550.m_CallState = i551[6]
  return i550
}

Deserializers["UnityEngine.UI.Navigation"] = function (request, data, root) {
  var i552 = root || request.c( 'UnityEngine.UI.Navigation' )
  var i553 = data
  i552.m_Mode = i553[0]
  i552.m_WrapAround = !!i553[1]
  request.r(i553[2], i553[3], 0, i552, 'm_SelectOnUp')
  request.r(i553[4], i553[5], 0, i552, 'm_SelectOnDown')
  request.r(i553[6], i553[7], 0, i552, 'm_SelectOnLeft')
  request.r(i553[8], i553[9], 0, i552, 'm_SelectOnRight')
  return i552
}

Deserializers["UnityEngine.UI.ColorBlock"] = function (request, data, root) {
  var i554 = root || request.c( 'UnityEngine.UI.ColorBlock' )
  var i555 = data
  i554.m_NormalColor = new pc.Color(i555[0], i555[1], i555[2], i555[3])
  i554.m_HighlightedColor = new pc.Color(i555[4], i555[5], i555[6], i555[7])
  i554.m_PressedColor = new pc.Color(i555[8], i555[9], i555[10], i555[11])
  i554.m_SelectedColor = new pc.Color(i555[12], i555[13], i555[14], i555[15])
  i554.m_DisabledColor = new pc.Color(i555[16], i555[17], i555[18], i555[19])
  i554.m_ColorMultiplier = i555[20]
  i554.m_FadeDuration = i555[21]
  return i554
}

Deserializers["UnityEngine.UI.SpriteState"] = function (request, data, root) {
  var i556 = root || request.c( 'UnityEngine.UI.SpriteState' )
  var i557 = data
  request.r(i557[0], i557[1], 0, i556, 'm_HighlightedSprite')
  request.r(i557[2], i557[3], 0, i556, 'm_PressedSprite')
  request.r(i557[4], i557[5], 0, i556, 'm_SelectedSprite')
  request.r(i557[6], i557[7], 0, i556, 'm_DisabledSprite')
  return i556
}

Deserializers["UnityEngine.UI.AnimationTriggers"] = function (request, data, root) {
  var i558 = root || request.c( 'UnityEngine.UI.AnimationTriggers' )
  var i559 = data
  i558.m_NormalTrigger = i559[0]
  i558.m_HighlightedTrigger = i559[1]
  i558.m_PressedTrigger = i559[2]
  i558.m_SelectedTrigger = i559[3]
  i558.m_DisabledTrigger = i559[4]
  return i558
}

Deserializers["UnityEngine.UI.Text"] = function (request, data, root) {
  var i560 = root || request.c( 'UnityEngine.UI.Text' )
  var i561 = data
  i560.m_FontData = request.d('UnityEngine.UI.FontData', i561[0], i560.m_FontData)
  i560.m_Text = i561[1]
  request.r(i561[2], i561[3], 0, i560, 'm_Material')
  i560.m_Maskable = !!i561[4]
  i560.m_Color = new pc.Color(i561[5], i561[6], i561[7], i561[8])
  i560.m_RaycastTarget = !!i561[9]
  i560.m_RaycastPadding = new pc.Vec4( i561[10], i561[11], i561[12], i561[13] )
  return i560
}

Deserializers["UnityEngine.UI.FontData"] = function (request, data, root) {
  var i562 = root || request.c( 'UnityEngine.UI.FontData' )
  var i563 = data
  request.r(i563[0], i563[1], 0, i562, 'm_Font')
  i562.m_FontSize = i563[2]
  i562.m_FontStyle = i563[3]
  i562.m_BestFit = !!i563[4]
  i562.m_MinSize = i563[5]
  i562.m_MaxSize = i563[6]
  i562.m_Alignment = i563[7]
  i562.m_AlignByGeometry = !!i563[8]
  i562.m_RichText = !!i563[9]
  i562.m_HorizontalOverflow = i563[10]
  i562.m_VerticalOverflow = i563[11]
  i562.m_LineSpacing = i563[12]
  return i562
}

Deserializers["ECS_MagicTile.ScoreEffectController"] = function (request, data, root) {
  var i564 = root || request.c( 'ECS_MagicTile.ScoreEffectController' )
  var i565 = data
  request.r(i565[0], i565[1], 0, i564, 'scoreSignalEffectChannel')
  request.r(i565[2], i565[3], 0, i564, 'perfectScorePrefab')
  request.r(i565[4], i565[5], 0, i564, 'greatScorePrefab')
  request.r(i565[6], i565[7], 0, i564, 'burstMovementUIController')
  var i567 = i565[8]
  var i566 = []
  for(var i = 0; i < i567.length; i += 1) {
    i566.push( request.d('BurstMovementUIController+BurstMovementElement', i567[i + 0]) );
  }
  i564.burstMovementElements = i566
  return i564
}

Deserializers["BurstMovementUIController+BurstMovementElement"] = function (request, data, root) {
  var i570 = root || request.c( 'BurstMovementUIController+BurstMovementElement' )
  var i571 = data
  request.r(i571[0], i571[1], 0, i570, 'target')
  i570.data = request.d('BurstMovementUIController+BurstMovementData', i571[2], i570.data)
  return i570
}

Deserializers["BurstMovementUIController+BurstMovementData"] = function (request, data, root) {
  var i572 = root || request.c( 'BurstMovementUIController+BurstMovementData' )
  var i573 = data
  i572.direction = new pc.Vec2( i573[0], i573[1] )
  i572.maxDistance = i573[2]
  i572.maxSpeed = i573[3]
  i572.accelerationFactor = i573[4]
  i572.decelerationFactor = i573[5]
  i572.burstEndPercentage = i573[6]
  i572.initialSpeedPercent = i573[7]
  i572.startPosition = new pc.Vec2( i573[8], i573[9] )
  i572.currentSpeed = i573[10]
  i572.hasStarted = !!i573[11]
  i572.isFinished = !!i573[12]
  return i572
}

Deserializers["BurstMovementUIController"] = function (request, data, root) {
  var i574 = root || request.c( 'BurstMovementUIController' )
  var i575 = data
  i574.defaultMaxSpeed = i575[0]
  i574.defaultAccelerationFactor = i575[1]
  i574.defaultDecelerationFactor = i575[2]
  i574.defaultBurstEndPercentage = i575[3]
  i574.defaultInitialSpeedPercent = i575[4]
  return i574
}

Deserializers["ECS_MagicTile.StarTween"] = function (request, data, root) {
  var i576 = root || request.c( 'ECS_MagicTile.StarTween' )
  var i577 = data
  i576.defaultValue = request.d('ECS_MagicTile.StarTween+StarProperties', i577[0], i576.defaultValue)
  return i576
}

Deserializers["ECS_MagicTile.StarTween+StarProperties"] = function (request, data, root) {
  var i578 = root || request.c( 'ECS_MagicTile.StarTween+StarProperties' )
  var i579 = data
  request.r(i579[0], i579[1], 0, i578, 'starRect')
  request.r(i579[2], i579[3], 0, i578, 'starAwakenedImg')
  i578.scaleStartValue = i579[4]
  i578.scaleMidValue = i579[5]
  i578.scaleEndValue = i579[6]
  i578.rotationStartValue = new pc.Vec3( i579[7], i579[8], i579[9] )
  i578.rotationMidValue = new pc.Vec3( i579[10], i579[11], i579[12] )
  i578.rotationEndValue = new pc.Vec3( i579[13], i579[14], i579[15] )
  i578.awakenedScaleStart = i579[16]
  i578.awakenedScaleMid = i579[17]
  i578.awakenedScaleEnd = i579[18]
  i578.alphaStart = i579[19]
  i578.alphaMid = i579[20]
  i578.alphaEnd = i579[21]
  i578.firstPhaseDuration = i579[22]
  i578.firstPhaseEase = i579[23]
  i578.secondPhaseDuration = i579[24]
  i578.secondPhaseEase = i579[25]
  return i578
}

Deserializers["ECS_MagicTile.CrownTween"] = function (request, data, root) {
  var i580 = root || request.c( 'ECS_MagicTile.CrownTween' )
  var i581 = data
  i580.defaultValue = request.d('ECS_MagicTile.CrownTween+CrownProperties', i581[0], i580.defaultValue)
  return i580
}

Deserializers["ECS_MagicTile.CrownTween+CrownProperties"] = function (request, data, root) {
  var i582 = root || request.c( 'ECS_MagicTile.CrownTween+CrownProperties' )
  var i583 = data
  request.r(i583[0], i583[1], 0, i582, 'crownRect')
  request.r(i583[2], i583[3], 0, i582, 'crownAwakenedImg')
  i582.scaleStartValue = i583[4]
  i582.scaleMidValue = i583[5]
  i582.scaleEndValue = i583[6]
  i582.awakenedScaleStart = i583[7]
  i582.awakenedScaleMid = i583[8]
  i582.awakenedScaleEnd = i583[9]
  i582.alphaStart = i583[10]
  i582.alphaMid = i583[11]
  i582.alphaEnd = i583[12]
  i582.firstPhaseDuration = i583[13]
  i582.firstPhaseEase = i583[14]
  i582.secondPhaseDuration = i583[15]
  i582.secondPhaseEase = i583[16]
  return i582
}

Deserializers["ECS_MagicTile.ProgressEffectController"] = function (request, data, root) {
  var i584 = root || request.c( 'ECS_MagicTile.ProgressEffectController' )
  var i585 = data
  request.r(i585[0], i585[1], 0, i584, 'progressSlider')
  var i587 = i585[2]
  var i586 = []
  for(var i = 0; i < i587.length; i += 2) {
  request.r(i587[i + 0], i587[i + 1], 2, i586, '')
  }
  i584.progressPoints = i586
  var i589 = i585[3]
  var i588 = []
  for(var i = 0; i < i589.length; i += 1) {
    i588.push( request.d('ECS_MagicTile.StarTween+StarProperties', i589[i + 0]) );
  }
  i584.starPoints = i588
  var i591 = i585[4]
  var i590 = []
  for(var i = 0; i < i591.length; i += 1) {
    i590.push( request.d('ECS_MagicTile.CrownTween+CrownProperties', i591[i + 0]) );
  }
  i584.crownPoints = i590
  return i584
}

Deserializers["UnityEngine.UI.Slider"] = function (request, data, root) {
  var i598 = root || request.c( 'UnityEngine.UI.Slider' )
  var i599 = data
  request.r(i599[0], i599[1], 0, i598, 'm_FillRect')
  request.r(i599[2], i599[3], 0, i598, 'm_HandleRect')
  i598.m_Direction = i599[4]
  i598.m_MinValue = i599[5]
  i598.m_MaxValue = i599[6]
  i598.m_WholeNumbers = !!i599[7]
  i598.m_Value = i599[8]
  i598.m_OnValueChanged = request.d('UnityEngine.UI.Slider+SliderEvent', i599[9], i598.m_OnValueChanged)
  i598.m_Navigation = request.d('UnityEngine.UI.Navigation', i599[10], i598.m_Navigation)
  i598.m_Transition = i599[11]
  i598.m_Colors = request.d('UnityEngine.UI.ColorBlock', i599[12], i598.m_Colors)
  i598.m_SpriteState = request.d('UnityEngine.UI.SpriteState', i599[13], i598.m_SpriteState)
  i598.m_AnimationTriggers = request.d('UnityEngine.UI.AnimationTriggers', i599[14], i598.m_AnimationTriggers)
  i598.m_Interactable = !!i599[15]
  request.r(i599[16], i599[17], 0, i598, 'm_TargetGraphic')
  return i598
}

Deserializers["UnityEngine.UI.Slider+SliderEvent"] = function (request, data, root) {
  var i600 = root || request.c( 'UnityEngine.UI.Slider+SliderEvent' )
  var i601 = data
  i600.m_PersistentCalls = request.d('UnityEngine.Events.PersistentCallGroup', i601[0], i600.m_PersistentCalls)
  return i600
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.ParticleSystem"] = function (request, data, root) {
  var i602 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.ParticleSystem' )
  var i603 = data
  i602.main = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.MainModule', i603[0], i602.main)
  i602.colorBySpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorBySpeedModule', i603[1], i602.colorBySpeed)
  i602.colorOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorOverLifetimeModule', i603[2], i602.colorOverLifetime)
  i602.emission = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.EmissionModule', i603[3], i602.emission)
  i602.rotationBySpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationBySpeedModule', i603[4], i602.rotationBySpeed)
  i602.rotationOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationOverLifetimeModule', i603[5], i602.rotationOverLifetime)
  i602.shape = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ShapeModule', i603[6], i602.shape)
  i602.sizeBySpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeBySpeedModule', i603[7], i602.sizeBySpeed)
  i602.sizeOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeOverLifetimeModule', i603[8], i602.sizeOverLifetime)
  i602.textureSheetAnimation = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.TextureSheetAnimationModule', i603[9], i602.textureSheetAnimation)
  i602.velocityOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.VelocityOverLifetimeModule', i603[10], i602.velocityOverLifetime)
  i602.noise = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.NoiseModule', i603[11], i602.noise)
  i602.inheritVelocity = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.InheritVelocityModule', i603[12], i602.inheritVelocity)
  i602.forceOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ForceOverLifetimeModule', i603[13], i602.forceOverLifetime)
  i602.limitVelocityOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.LimitVelocityOverLifetimeModule', i603[14], i602.limitVelocityOverLifetime)
  i602.useAutoRandomSeed = !!i603[15]
  i602.randomSeed = i603[16]
  return i602
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.MainModule"] = function (request, data, root) {
  var i604 = root || new pc.ParticleSystemMain()
  var i605 = data
  i604.duration = i605[0]
  i604.loop = !!i605[1]
  i604.prewarm = !!i605[2]
  i604.startDelay = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i605[3], i604.startDelay)
  i604.startLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i605[4], i604.startLifetime)
  i604.startSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i605[5], i604.startSpeed)
  i604.startSize3D = !!i605[6]
  i604.startSizeX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i605[7], i604.startSizeX)
  i604.startSizeY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i605[8], i604.startSizeY)
  i604.startSizeZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i605[9], i604.startSizeZ)
  i604.startRotation3D = !!i605[10]
  i604.startRotationX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i605[11], i604.startRotationX)
  i604.startRotationY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i605[12], i604.startRotationY)
  i604.startRotationZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i605[13], i604.startRotationZ)
  i604.startColor = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient', i605[14], i604.startColor)
  i604.gravityModifier = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i605[15], i604.gravityModifier)
  i604.simulationSpace = i605[16]
  request.r(i605[17], i605[18], 0, i604, 'customSimulationSpace')
  i604.simulationSpeed = i605[19]
  i604.useUnscaledTime = !!i605[20]
  i604.scalingMode = i605[21]
  i604.playOnAwake = !!i605[22]
  i604.maxParticles = i605[23]
  i604.emitterVelocityMode = i605[24]
  i604.stopAction = i605[25]
  return i604
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve"] = function (request, data, root) {
  var i606 = root || new pc.MinMaxCurve()
  var i607 = data
  i606.mode = i607[0]
  i606.curveMin = new pc.AnimationCurve( { keys_flow: i607[1] } )
  i606.curveMax = new pc.AnimationCurve( { keys_flow: i607[2] } )
  i606.curveMultiplier = i607[3]
  i606.constantMin = i607[4]
  i606.constantMax = i607[5]
  return i606
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient"] = function (request, data, root) {
  var i608 = root || new pc.MinMaxGradient()
  var i609 = data
  i608.mode = i609[0]
  i608.gradientMin = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient', i609[1], i608.gradientMin)
  i608.gradientMax = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient', i609[2], i608.gradientMax)
  i608.colorMin = new pc.Color(i609[3], i609[4], i609[5], i609[6])
  i608.colorMax = new pc.Color(i609[7], i609[8], i609[9], i609[10])
  return i608
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient"] = function (request, data, root) {
  var i610 = root || request.c( 'Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient' )
  var i611 = data
  i610.mode = i611[0]
  var i613 = i611[1]
  var i612 = []
  for(var i = 0; i < i613.length; i += 1) {
    i612.push( request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey', i613[i + 0]) );
  }
  i610.colorKeys = i612
  var i615 = i611[2]
  var i614 = []
  for(var i = 0; i < i615.length; i += 1) {
    i614.push( request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey', i615[i + 0]) );
  }
  i610.alphaKeys = i614
  return i610
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorBySpeedModule"] = function (request, data, root) {
  var i616 = root || new pc.ParticleSystemColorBySpeed()
  var i617 = data
  i616.enabled = !!i617[0]
  i616.color = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient', i617[1], i616.color)
  i616.range = new pc.Vec2( i617[2], i617[3] )
  return i616
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey"] = function (request, data, root) {
  var i620 = root || request.c( 'Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey' )
  var i621 = data
  i620.color = new pc.Color(i621[0], i621[1], i621[2], i621[3])
  i620.time = i621[4]
  return i620
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey"] = function (request, data, root) {
  var i624 = root || request.c( 'Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey' )
  var i625 = data
  i624.alpha = i625[0]
  i624.time = i625[1]
  return i624
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorOverLifetimeModule"] = function (request, data, root) {
  var i626 = root || new pc.ParticleSystemColorOverLifetime()
  var i627 = data
  i626.enabled = !!i627[0]
  i626.color = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient', i627[1], i626.color)
  return i626
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.EmissionModule"] = function (request, data, root) {
  var i628 = root || new pc.ParticleSystemEmitter()
  var i629 = data
  i628.enabled = !!i629[0]
  i628.rateOverTime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i629[1], i628.rateOverTime)
  i628.rateOverDistance = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i629[2], i628.rateOverDistance)
  var i631 = i629[3]
  var i630 = []
  for(var i = 0; i < i631.length; i += 1) {
    i630.push( request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Burst', i631[i + 0]) );
  }
  i628.bursts = i630
  return i628
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Burst"] = function (request, data, root) {
  var i634 = root || new pc.ParticleSystemBurst()
  var i635 = data
  i634.count = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i635[0], i634.count)
  i634.cycleCount = i635[1]
  i634.minCount = i635[2]
  i634.maxCount = i635[3]
  i634.repeatInterval = i635[4]
  i634.time = i635[5]
  return i634
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationBySpeedModule"] = function (request, data, root) {
  var i636 = root || new pc.ParticleSystemRotationBySpeed()
  var i637 = data
  i636.enabled = !!i637[0]
  i636.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i637[1], i636.x)
  i636.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i637[2], i636.y)
  i636.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i637[3], i636.z)
  i636.separateAxes = !!i637[4]
  i636.range = new pc.Vec2( i637[5], i637[6] )
  return i636
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationOverLifetimeModule"] = function (request, data, root) {
  var i638 = root || new pc.ParticleSystemRotationOverLifetime()
  var i639 = data
  i638.enabled = !!i639[0]
  i638.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i639[1], i638.x)
  i638.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i639[2], i638.y)
  i638.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i639[3], i638.z)
  i638.separateAxes = !!i639[4]
  return i638
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ShapeModule"] = function (request, data, root) {
  var i640 = root || new pc.ParticleSystemShape()
  var i641 = data
  i640.enabled = !!i641[0]
  i640.shapeType = i641[1]
  i640.randomDirectionAmount = i641[2]
  i640.sphericalDirectionAmount = i641[3]
  i640.randomPositionAmount = i641[4]
  i640.alignToDirection = !!i641[5]
  i640.radius = i641[6]
  i640.radiusMode = i641[7]
  i640.radiusSpread = i641[8]
  i640.radiusSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i641[9], i640.radiusSpeed)
  i640.radiusThickness = i641[10]
  i640.angle = i641[11]
  i640.length = i641[12]
  i640.boxThickness = new pc.Vec3( i641[13], i641[14], i641[15] )
  i640.meshShapeType = i641[16]
  request.r(i641[17], i641[18], 0, i640, 'mesh')
  request.r(i641[19], i641[20], 0, i640, 'meshRenderer')
  request.r(i641[21], i641[22], 0, i640, 'skinnedMeshRenderer')
  i640.useMeshMaterialIndex = !!i641[23]
  i640.meshMaterialIndex = i641[24]
  i640.useMeshColors = !!i641[25]
  i640.normalOffset = i641[26]
  i640.arc = i641[27]
  i640.arcMode = i641[28]
  i640.arcSpread = i641[29]
  i640.arcSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i641[30], i640.arcSpeed)
  i640.donutRadius = i641[31]
  i640.position = new pc.Vec3( i641[32], i641[33], i641[34] )
  i640.rotation = new pc.Vec3( i641[35], i641[36], i641[37] )
  i640.scale = new pc.Vec3( i641[38], i641[39], i641[40] )
  return i640
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeBySpeedModule"] = function (request, data, root) {
  var i642 = root || new pc.ParticleSystemSizeBySpeed()
  var i643 = data
  i642.enabled = !!i643[0]
  i642.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i643[1], i642.x)
  i642.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i643[2], i642.y)
  i642.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i643[3], i642.z)
  i642.separateAxes = !!i643[4]
  i642.range = new pc.Vec2( i643[5], i643[6] )
  return i642
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeOverLifetimeModule"] = function (request, data, root) {
  var i644 = root || new pc.ParticleSystemSizeOverLifetime()
  var i645 = data
  i644.enabled = !!i645[0]
  i644.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i645[1], i644.x)
  i644.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i645[2], i644.y)
  i644.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i645[3], i644.z)
  i644.separateAxes = !!i645[4]
  return i644
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.TextureSheetAnimationModule"] = function (request, data, root) {
  var i646 = root || new pc.ParticleSystemTextureSheetAnimation()
  var i647 = data
  i646.enabled = !!i647[0]
  i646.mode = i647[1]
  i646.animation = i647[2]
  i646.numTilesX = i647[3]
  i646.numTilesY = i647[4]
  i646.useRandomRow = !!i647[5]
  i646.frameOverTime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i647[6], i646.frameOverTime)
  i646.startFrame = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i647[7], i646.startFrame)
  i646.cycleCount = i647[8]
  i646.rowIndex = i647[9]
  i646.flipU = i647[10]
  i646.flipV = i647[11]
  i646.spriteCount = i647[12]
  var i649 = i647[13]
  var i648 = []
  for(var i = 0; i < i649.length; i += 2) {
  request.r(i649[i + 0], i649[i + 1], 2, i648, '')
  }
  i646.sprites = i648
  return i646
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.VelocityOverLifetimeModule"] = function (request, data, root) {
  var i652 = root || new pc.ParticleSystemVelocityOverLifetime()
  var i653 = data
  i652.enabled = !!i653[0]
  i652.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i653[1], i652.x)
  i652.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i653[2], i652.y)
  i652.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i653[3], i652.z)
  i652.radial = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i653[4], i652.radial)
  i652.speedModifier = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i653[5], i652.speedModifier)
  i652.space = i653[6]
  i652.orbitalX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i653[7], i652.orbitalX)
  i652.orbitalY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i653[8], i652.orbitalY)
  i652.orbitalZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i653[9], i652.orbitalZ)
  i652.orbitalOffsetX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i653[10], i652.orbitalOffsetX)
  i652.orbitalOffsetY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i653[11], i652.orbitalOffsetY)
  i652.orbitalOffsetZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i653[12], i652.orbitalOffsetZ)
  return i652
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.NoiseModule"] = function (request, data, root) {
  var i654 = root || new pc.ParticleSystemNoise()
  var i655 = data
  i654.enabled = !!i655[0]
  i654.separateAxes = !!i655[1]
  i654.strengthX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i655[2], i654.strengthX)
  i654.strengthY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i655[3], i654.strengthY)
  i654.strengthZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i655[4], i654.strengthZ)
  i654.frequency = i655[5]
  i654.damping = !!i655[6]
  i654.octaveCount = i655[7]
  i654.octaveMultiplier = i655[8]
  i654.octaveScale = i655[9]
  i654.quality = i655[10]
  i654.scrollSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i655[11], i654.scrollSpeed)
  i654.scrollSpeedMultiplier = i655[12]
  i654.remapEnabled = !!i655[13]
  i654.remapX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i655[14], i654.remapX)
  i654.remapY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i655[15], i654.remapY)
  i654.remapZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i655[16], i654.remapZ)
  i654.positionAmount = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i655[17], i654.positionAmount)
  i654.rotationAmount = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i655[18], i654.rotationAmount)
  i654.sizeAmount = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i655[19], i654.sizeAmount)
  return i654
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.InheritVelocityModule"] = function (request, data, root) {
  var i656 = root || new pc.ParticleSystemInheritVelocity()
  var i657 = data
  i656.enabled = !!i657[0]
  i656.mode = i657[1]
  i656.curve = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i657[2], i656.curve)
  return i656
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ForceOverLifetimeModule"] = function (request, data, root) {
  var i658 = root || new pc.ParticleSystemForceOverLifetime()
  var i659 = data
  i658.enabled = !!i659[0]
  i658.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i659[1], i658.x)
  i658.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i659[2], i658.y)
  i658.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i659[3], i658.z)
  i658.space = i659[4]
  i658.randomized = !!i659[5]
  return i658
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.LimitVelocityOverLifetimeModule"] = function (request, data, root) {
  var i660 = root || new pc.ParticleSystemLimitVelocityOverLifetime()
  var i661 = data
  i660.enabled = !!i661[0]
  i660.limit = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i661[1], i660.limit)
  i660.limitX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i661[2], i660.limitX)
  i660.limitY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i661[3], i660.limitY)
  i660.limitZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i661[4], i660.limitZ)
  i660.dampen = i661[5]
  i660.separateAxes = !!i661[6]
  i660.space = i661[7]
  i660.drag = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i661[8], i660.drag)
  i660.multiplyDragByParticleSize = !!i661[9]
  i660.multiplyDragByParticleVelocity = !!i661[10]
  return i660
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.ParticleSystemRenderer"] = function (request, data, root) {
  var i662 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.ParticleSystemRenderer' )
  var i663 = data
  i662.enabled = !!i663[0]
  request.r(i663[1], i663[2], 0, i662, 'sharedMaterial')
  var i665 = i663[3]
  var i664 = []
  for(var i = 0; i < i665.length; i += 2) {
  request.r(i665[i + 0], i665[i + 1], 2, i664, '')
  }
  i662.sharedMaterials = i664
  i662.receiveShadows = !!i663[4]
  i662.shadowCastingMode = i663[5]
  i662.sortingLayerID = i663[6]
  i662.sortingOrder = i663[7]
  i662.lightmapIndex = i663[8]
  i662.lightmapSceneIndex = i663[9]
  i662.lightmapScaleOffset = new pc.Vec4( i663[10], i663[11], i663[12], i663[13] )
  i662.lightProbeUsage = i663[14]
  i662.reflectionProbeUsage = i663[15]
  request.r(i663[16], i663[17], 0, i662, 'mesh')
  i662.meshCount = i663[18]
  i662.activeVertexStreamsCount = i663[19]
  i662.alignment = i663[20]
  i662.renderMode = i663[21]
  i662.sortMode = i663[22]
  i662.lengthScale = i663[23]
  i662.velocityScale = i663[24]
  i662.cameraVelocityScale = i663[25]
  i662.normalDirection = i663[26]
  i662.sortingFudge = i663[27]
  i662.minParticleSize = i663[28]
  i662.maxParticleSize = i663[29]
  i662.pivot = new pc.Vec3( i663[30], i663[31], i663[32] )
  request.r(i663[33], i663[34], 0, i662, 'trailMaterial')
  return i662
}

Deserializers["ECS_MagicTile.EffectOnProgress"] = function (request, data, root) {
  var i666 = root || request.c( 'ECS_MagicTile.EffectOnProgress' )
  var i667 = data
  i666.startScale = new pc.Vec2( i667[0], i667[1] )
  i666.endScale = new pc.Vec2( i667[2], i667[3] )
  i666.duration = i667[4]
  return i666
}

Deserializers["UnityEngine.EventSystems.EventSystem"] = function (request, data, root) {
  var i668 = root || request.c( 'UnityEngine.EventSystems.EventSystem' )
  var i669 = data
  request.r(i669[0], i669[1], 0, i668, 'm_FirstSelected')
  i668.m_sendNavigationEvents = !!i669[2]
  i668.m_DragThreshold = i669[3]
  return i668
}

Deserializers["UnityEngine.EventSystems.StandaloneInputModule"] = function (request, data, root) {
  var i670 = root || request.c( 'UnityEngine.EventSystems.StandaloneInputModule' )
  var i671 = data
  i670.m_HorizontalAxis = i671[0]
  i670.m_VerticalAxis = i671[1]
  i670.m_SubmitButton = i671[2]
  i670.m_CancelButton = i671[3]
  i670.m_InputActionsPerSecond = i671[4]
  i670.m_RepeatDelay = i671[5]
  i670.m_ForceModuleActive = !!i671[6]
  i670.m_SendPointerHoverToParent = !!i671[7]
  return i670
}

Deserializers["ECS_MagicTile.ScreenManager"] = function (request, data, root) {
  var i672 = root || request.c( 'ECS_MagicTile.ScreenManager' )
  var i673 = data
  request.r(i673[0], i673[1], 0, i672, 'OnOrientationChange')
  return i672
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.AudioSource"] = function (request, data, root) {
  var i674 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.AudioSource' )
  var i675 = data
  request.r(i675[0], i675[1], 0, i674, 'clip')
  request.r(i675[2], i675[3], 0, i674, 'outputAudioMixerGroup')
  i674.playOnAwake = !!i675[4]
  i674.loop = !!i675[5]
  i674.time = i675[6]
  i674.volume = i675[7]
  i674.pitch = i675[8]
  i674.enabled = !!i675[9]
  return i674
}

Deserializers["ECS_MagicTile.AudioManager"] = function (request, data, root) {
  var i676 = root || request.c( 'ECS_MagicTile.AudioManager' )
  var i677 = data
  request.r(i677[0], i677[1], 0, i676, 'onSongStartChannel')
  request.r(i677[2], i677[3], 0, i676, 'audioClip')
  return i676
}

Deserializers["Facade.Tweening.TweenManager"] = function (request, data, root) {
  var i678 = root || request.c( 'Facade.Tweening.TweenManager' )
  var i679 = data
  i678._currentLibrary = i679[0]
  return i678
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings"] = function (request, data, root) {
  var i680 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderSettings' )
  var i681 = data
  i680.ambientIntensity = i681[0]
  i680.reflectionIntensity = i681[1]
  i680.ambientMode = i681[2]
  i680.ambientLight = new pc.Color(i681[3], i681[4], i681[5], i681[6])
  i680.ambientSkyColor = new pc.Color(i681[7], i681[8], i681[9], i681[10])
  i680.ambientGroundColor = new pc.Color(i681[11], i681[12], i681[13], i681[14])
  i680.ambientEquatorColor = new pc.Color(i681[15], i681[16], i681[17], i681[18])
  i680.fogColor = new pc.Color(i681[19], i681[20], i681[21], i681[22])
  i680.fogEndDistance = i681[23]
  i680.fogStartDistance = i681[24]
  i680.fogDensity = i681[25]
  i680.fog = !!i681[26]
  request.r(i681[27], i681[28], 0, i680, 'skybox')
  i680.fogMode = i681[29]
  var i683 = i681[30]
  var i682 = []
  for(var i = 0; i < i683.length; i += 1) {
    i682.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap', i683[i + 0]) );
  }
  i680.lightmaps = i682
  i680.lightProbes = request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes', i681[31], i680.lightProbes)
  i680.lightmapsMode = i681[32]
  i680.mixedBakeMode = i681[33]
  i680.environmentLightingMode = i681[34]
  i680.ambientProbe = new pc.SphericalHarmonicsL2(i681[35])
  i680.referenceAmbientProbe = new pc.SphericalHarmonicsL2(i681[36])
  i680.useReferenceAmbientProbe = !!i681[37]
  request.r(i681[38], i681[39], 0, i680, 'customReflection')
  request.r(i681[40], i681[41], 0, i680, 'defaultReflection')
  i680.defaultReflectionMode = i681[42]
  i680.defaultReflectionResolution = i681[43]
  i680.sunLightObjectId = i681[44]
  i680.pixelLightCount = i681[45]
  i680.defaultReflectionHDR = !!i681[46]
  i680.hasLightDataAsset = !!i681[47]
  i680.hasManualGenerate = !!i681[48]
  return i680
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap"] = function (request, data, root) {
  var i686 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap' )
  var i687 = data
  request.r(i687[0], i687[1], 0, i686, 'lightmapColor')
  request.r(i687[2], i687[3], 0, i686, 'lightmapDirection')
  return i686
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes"] = function (request, data, root) {
  var i688 = root || new UnityEngine.LightProbes()
  var i689 = data
  return i688
}

Deserializers["GlobalGameSetting"] = function (request, data, root) {
  var i696 = root || request.c( 'GlobalGameSetting' )
  var i697 = data
  request.r(i697[0], i697[1], 0, i696, 'generalSetting')
  request.r(i697[2], i697[3], 0, i696, 'dataSystemSetting')
  request.r(i697[4], i697[5], 0, i696, 'presenterSetting')
  request.r(i697[6], i697[7], 0, i696, 'perfectLineSettingSO')
  request.r(i697[8], i697[9], 0, i696, 'musicNoteSettingSO')
  request.r(i697[10], i697[11], 0, i696, 'notePresenterParent')
  request.r(i697[12], i697[13], 0, i696, 'inputPresenterParent')
  request.r(i697[14], i697[15], 0, i696, 'laneLineSettings')
  request.r(i697[16], i697[17], 0, i696, 'introNoteSetting')
  return i696
}

Deserializers["GizmoDebugger"] = function (request, data, root) {
  var i698 = root || request.c( 'GizmoDebugger' )
  var i699 = data
  i698.gizmosSize = i699[0]
  return i698
}

Deserializers["ManualDebug"] = function (request, data, root) {
  var i700 = root || request.c( 'ManualDebug' )
  var i701 = data
  i700.triggerKey = i701[0]
  i700.enableDebugging = !!i701[1]
  return i700
}

Deserializers["MusicTileManager"] = function (request, data, root) {
  var i702 = root || request.c( 'MusicTileManager' )
  var i703 = data
  return i702
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader"] = function (request, data, root) {
  var i704 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader' )
  var i705 = data
  var i707 = i705[0]
  var i706 = new (System.Collections.Generic.List$1(Bridge.ns('Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError')))
  for(var i = 0; i < i707.length; i += 1) {
    i706.add(request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError', i707[i + 0]));
  }
  i704.ShaderCompilationErrors = i706
  i704.name = i705[1]
  i704.guid = i705[2]
  var i709 = i705[3]
  var i708 = []
  for(var i = 0; i < i709.length; i += 1) {
    i708.push( i709[i + 0] );
  }
  i704.shaderDefinedKeywords = i708
  var i711 = i705[4]
  var i710 = []
  for(var i = 0; i < i711.length; i += 1) {
    i710.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass', i711[i + 0]) );
  }
  i704.passes = i710
  var i713 = i705[5]
  var i712 = []
  for(var i = 0; i < i713.length; i += 1) {
    i712.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass', i713[i + 0]) );
  }
  i704.usePasses = i712
  var i715 = i705[6]
  var i714 = []
  for(var i = 0; i < i715.length; i += 1) {
    i714.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue', i715[i + 0]) );
  }
  i704.defaultParameterValues = i714
  request.r(i705[7], i705[8], 0, i704, 'unityFallbackShader')
  i704.readDepth = !!i705[9]
  i704.isCreatedByShaderGraph = !!i705[10]
  i704.compiled = !!i705[11]
  return i704
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError"] = function (request, data, root) {
  var i718 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError' )
  var i719 = data
  i718.shaderName = i719[0]
  i718.errorMessage = i719[1]
  return i718
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass"] = function (request, data, root) {
  var i724 = root || new pc.UnityShaderPass()
  var i725 = data
  i724.id = i725[0]
  i724.subShaderIndex = i725[1]
  i724.name = i725[2]
  i724.passType = i725[3]
  i724.grabPassTextureName = i725[4]
  i724.usePass = !!i725[5]
  i724.zTest = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i725[6], i724.zTest)
  i724.zWrite = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i725[7], i724.zWrite)
  i724.culling = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i725[8], i724.culling)
  i724.blending = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending', i725[9], i724.blending)
  i724.alphaBlending = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending', i725[10], i724.alphaBlending)
  i724.colorWriteMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i725[11], i724.colorWriteMask)
  i724.offsetUnits = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i725[12], i724.offsetUnits)
  i724.offsetFactor = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i725[13], i724.offsetFactor)
  i724.stencilRef = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i725[14], i724.stencilRef)
  i724.stencilReadMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i725[15], i724.stencilReadMask)
  i724.stencilWriteMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i725[16], i724.stencilWriteMask)
  i724.stencilOp = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i725[17], i724.stencilOp)
  i724.stencilOpFront = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i725[18], i724.stencilOpFront)
  i724.stencilOpBack = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i725[19], i724.stencilOpBack)
  var i727 = i725[20]
  var i726 = []
  for(var i = 0; i < i727.length; i += 1) {
    i726.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag', i727[i + 0]) );
  }
  i724.tags = i726
  var i729 = i725[21]
  var i728 = []
  for(var i = 0; i < i729.length; i += 1) {
    i728.push( i729[i + 0] );
  }
  i724.passDefinedKeywords = i728
  var i731 = i725[22]
  var i730 = []
  for(var i = 0; i < i731.length; i += 1) {
    i730.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup', i731[i + 0]) );
  }
  i724.passDefinedKeywordGroups = i730
  var i733 = i725[23]
  var i732 = []
  for(var i = 0; i < i733.length; i += 1) {
    i732.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant', i733[i + 0]) );
  }
  i724.variants = i732
  var i735 = i725[24]
  var i734 = []
  for(var i = 0; i < i735.length; i += 1) {
    i734.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant', i735[i + 0]) );
  }
  i724.excludedVariants = i734
  i724.hasDepthReader = !!i725[25]
  return i724
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value"] = function (request, data, root) {
  var i736 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value' )
  var i737 = data
  i736.val = i737[0]
  i736.name = i737[1]
  return i736
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending"] = function (request, data, root) {
  var i738 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending' )
  var i739 = data
  i738.src = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i739[0], i738.src)
  i738.dst = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i739[1], i738.dst)
  i738.op = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i739[2], i738.op)
  return i738
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp"] = function (request, data, root) {
  var i740 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp' )
  var i741 = data
  i740.pass = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i741[0], i740.pass)
  i740.fail = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i741[1], i740.fail)
  i740.zFail = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i741[2], i740.zFail)
  i740.comp = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i741[3], i740.comp)
  return i740
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag"] = function (request, data, root) {
  var i744 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag' )
  var i745 = data
  i744.name = i745[0]
  i744.value = i745[1]
  return i744
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup"] = function (request, data, root) {
  var i748 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup' )
  var i749 = data
  var i751 = i749[0]
  var i750 = []
  for(var i = 0; i < i751.length; i += 1) {
    i750.push( i751[i + 0] );
  }
  i748.keywords = i750
  i748.hasDiscard = !!i749[1]
  return i748
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant"] = function (request, data, root) {
  var i754 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant' )
  var i755 = data
  i754.passId = i755[0]
  i754.subShaderIndex = i755[1]
  var i757 = i755[2]
  var i756 = []
  for(var i = 0; i < i757.length; i += 1) {
    i756.push( i757[i + 0] );
  }
  i754.keywords = i756
  i754.vertexProgram = i755[3]
  i754.fragmentProgram = i755[4]
  i754.exportedForWebGl2 = !!i755[5]
  i754.readDepth = !!i755[6]
  return i754
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass"] = function (request, data, root) {
  var i760 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass' )
  var i761 = data
  request.r(i761[0], i761[1], 0, i760, 'shader')
  i760.pass = i761[2]
  return i760
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue"] = function (request, data, root) {
  var i764 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue' )
  var i765 = data
  i764.name = i765[0]
  i764.type = i765[1]
  i764.value = new pc.Vec4( i765[2], i765[3], i765[4], i765[5] )
  i764.textureValue = i765[6]
  i764.shaderPropertyFlag = i765[7]
  return i764
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Sprite"] = function (request, data, root) {
  var i766 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Sprite' )
  var i767 = data
  i766.name = i767[0]
  request.r(i767[1], i767[2], 0, i766, 'texture')
  i766.aabb = i767[3]
  i766.vertices = i767[4]
  i766.triangles = i767[5]
  i766.textureRect = UnityEngine.Rect.MinMaxRect(i767[6], i767[7], i767[8], i767[9])
  i766.packedRect = UnityEngine.Rect.MinMaxRect(i767[10], i767[11], i767[12], i767[13])
  i766.border = new pc.Vec4( i767[14], i767[15], i767[16], i767[17] )
  i766.transparency = i767[18]
  i766.bounds = i767[19]
  i766.pixelsPerUnit = i767[20]
  i766.textureWidth = i767[21]
  i766.textureHeight = i767[22]
  i766.nativeSize = new pc.Vec2( i767[23], i767[24] )
  i766.pivot = new pc.Vec2( i767[25], i767[26] )
  i766.textureRectOffset = new pc.Vec2( i767[27], i767[28] )
  return i766
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.AudioClip"] = function (request, data, root) {
  var i768 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.AudioClip' )
  var i769 = data
  i768.name = i769[0]
  return i768
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Font"] = function (request, data, root) {
  var i770 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Font' )
  var i771 = data
  i770.name = i771[0]
  i770.ascent = i771[1]
  i770.originalLineHeight = i771[2]
  i770.fontSize = i771[3]
  var i773 = i771[4]
  var i772 = []
  for(var i = 0; i < i773.length; i += 1) {
    i772.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo', i773[i + 0]) );
  }
  i770.characterInfo = i772
  request.r(i771[5], i771[6], 0, i770, 'texture')
  i770.originalFontSize = i771[7]
  return i770
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo"] = function (request, data, root) {
  var i776 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo' )
  var i777 = data
  i776.index = i777[0]
  i776.advance = i777[1]
  i776.bearing = i777[2]
  i776.glyphWidth = i777[3]
  i776.glyphHeight = i777[4]
  i776.minX = i777[5]
  i776.maxX = i777[6]
  i776.minY = i777[7]
  i776.maxY = i777[8]
  i776.uvBottomLeftX = i777[9]
  i776.uvBottomLeftY = i777[10]
  i776.uvBottomRightX = i777[11]
  i776.uvBottomRightY = i777[12]
  i776.uvTopLeftX = i777[13]
  i776.uvTopLeftY = i777[14]
  i776.uvTopRightX = i777[15]
  i776.uvTopRightY = i777[16]
  return i776
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.TextAsset"] = function (request, data, root) {
  var i778 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.TextAsset' )
  var i779 = data
  i778.name = i779[0]
  i778.bytes64 = i779[1]
  i778.data = i779[2]
  return i778
}

Deserializers["ECS_MagicTile.GeneralGameSetting"] = function (request, data, root) {
  var i780 = root || request.c( 'ECS_MagicTile.GeneralGameSetting' )
  var i781 = data
  i780.GameSpeed = i781[0]
  return i780
}

Deserializers["ECS_MagicTile.MusicNoteCreationSetting"] = function (request, data, root) {
  var i782 = root || request.c( 'ECS_MagicTile.MusicNoteCreationSetting' )
  var i783 = data
  i782.UsePreciseNoteCalculation = !!i783[0]
  request.r(i783[1], i783[2], 0, i782, 'MidiContent')
  i782.ShortNoteScaleYFactor = i783[3]
  i782.LongNoteScaleYFactor = i783[4]
  i782.startingNoteLane = i783[5]
  request.r(i783[6], i783[7], 0, i782, 'LongTilePrefab')
  request.r(i783[8], i783[9], 0, i782, 'ShortTilePrefab')
  request.r(i783[10], i783[11], 0, i782, 'startingNotePrefab')
  return i782
}

Deserializers["PerfectLineSettingSO"] = function (request, data, root) {
  var i784 = root || request.c( 'PerfectLineSettingSO' )
  var i785 = data
  i784.TopLeft = new pc.Vec2( i785[0], i785[1] )
  i784.TopRight = new pc.Vec2( i785[2], i785[3] )
  i784.BottomLeft = new pc.Vec2( i785[4], i785[5] )
  i784.BottomRight = new pc.Vec2( i785[6], i785[7] )
  i784.Position = new pc.Vec2( i785[8], i785[9] )
  return i784
}

Deserializers["ECS_MagicTile.PerfectLineSetting"] = function (request, data, root) {
  var i786 = root || request.c( 'ECS_MagicTile.PerfectLineSetting' )
  var i787 = data
  i786.portraitNormalizedPos = request.d('ECS_MagicTile.PerfectLineSetting+NormalizedFloatPreset', i787[0], i786.portraitNormalizedPos)
  i786.landscapeNormalizedPos = request.d('ECS_MagicTile.PerfectLineSetting+NormalizedFloatPreset', i787[1], i786.landscapeNormalizedPos)
  i786.portraitNormalizedSize = request.d('ECS_MagicTile.PerfectLineSetting+NormalizedFloatPreset', i787[2], i786.portraitNormalizedSize)
  i786.landscapeNormalizedSize = request.d('ECS_MagicTile.PerfectLineSetting+NormalizedFloatPreset', i787[3], i786.landscapeNormalizedSize)
  return i786
}

Deserializers["ECS_MagicTile.LaneLineSettings"] = function (request, data, root) {
  var i788 = root || request.c( 'ECS_MagicTile.LaneLineSettings' )
  var i789 = data
  request.r(i789[0], i789[1], 0, i788, 'landLinePrefab')
  i788.laneLineWidth = request.d('ECS_MagicTile.RangeReactiveFloat', i789[2], i788.laneLineWidth)
  return i788
}

Deserializers["EventChannel.IntEventChannel"] = function (request, data, root) {
  var i790 = root || request.c( 'EventChannel.IntEventChannel' )
  var i791 = data
  i790.maxListeners = i791[0]
  return i790
}

Deserializers["EventChannel.BoolEventChannel"] = function (request, data, root) {
  var i792 = root || request.c( 'EventChannel.BoolEventChannel' )
  var i793 = data
  i792.maxListeners = i793[0]
  return i792
}

Deserializers["EventChannel.EmptyEventChannel"] = function (request, data, root) {
  var i794 = root || request.c( 'EventChannel.EmptyEventChannel' )
  var i795 = data
  i794.maxListeners = i795[0]
  return i794
}

Deserializers["GeneralGameSettingSO"] = function (request, data, root) {
  var i796 = root || request.c( 'GeneralGameSettingSO' )
  var i797 = data
  i796.gameSpeed = i797[0]
  i796.baseScaleYForNote = i797[1]
  request.r(i797[2], i797[3], 0, i796, 'midiContent')
  return i796
}

Deserializers["DataSystemSettingSO"] = function (request, data, root) {
  var i798 = root || request.c( 'DataSystemSettingSO' )
  var i799 = data
  i798.defaultCapacity = i799[0]
  return i798
}

Deserializers["PresenterSettingSO"] = function (request, data, root) {
  var i800 = root || request.c( 'PresenterSettingSO' )
  var i801 = data
  request.r(i801[0], i801[1], 0, i800, 'shortMusicNotePresenterPrefab')
  request.r(i801[2], i801[3], 0, i800, 'longMusicNotePresenterPrefab')
  request.r(i801[4], i801[5], 0, i800, 'inputDebuggerPresenterPrefab')
  request.r(i801[6], i801[7], 0, i800, 'laneLinePresenter')
  request.r(i801[8], i801[9], 0, i800, 'introNotePressenyer')
  return i800
}

Deserializers["MusicNoteSettingSO"] = function (request, data, root) {
  var i802 = root || request.c( 'MusicNoteSettingSO' )
  var i803 = data
  i802.shortNoteScaleYFactor = i803[0]
  i802.longNoteScaleYFactor = i803[1]
  return i802
}

Deserializers["LaneLineSettingSO"] = function (request, data, root) {
  var i804 = root || request.c( 'LaneLineSettingSO' )
  var i805 = data
  i804.lineWidthPercentage = i805[0]
  i804.lineColor = new pc.Color(i805[1], i805[2], i805[3], i805[4])
  return i804
}

Deserializers["IntroNoteSettingSO"] = function (request, data, root) {
  var i806 = root || request.c( 'IntroNoteSettingSO' )
  var i807 = data
  i806.introNoteScaleYFactor = i807[0]
  i806.initLane = i807[1]
  return i806
}

Deserializers["DG.Tweening.Core.DOTweenSettings"] = function (request, data, root) {
  var i808 = root || request.c( 'DG.Tweening.Core.DOTweenSettings' )
  var i809 = data
  i808.useSafeMode = !!i809[0]
  i808.safeModeOptions = request.d('DG.Tweening.Core.DOTweenSettings+SafeModeOptions', i809[1], i808.safeModeOptions)
  i808.timeScale = i809[2]
  i808.unscaledTimeScale = i809[3]
  i808.useSmoothDeltaTime = !!i809[4]
  i808.maxSmoothUnscaledTime = i809[5]
  i808.rewindCallbackMode = i809[6]
  i808.showUnityEditorReport = !!i809[7]
  i808.logBehaviour = i809[8]
  i808.drawGizmos = !!i809[9]
  i808.defaultRecyclable = !!i809[10]
  i808.defaultAutoPlay = i809[11]
  i808.defaultUpdateType = i809[12]
  i808.defaultTimeScaleIndependent = !!i809[13]
  i808.defaultEaseType = i809[14]
  i808.defaultEaseOvershootOrAmplitude = i809[15]
  i808.defaultEasePeriod = i809[16]
  i808.defaultAutoKill = !!i809[17]
  i808.defaultLoopType = i809[18]
  i808.debugMode = !!i809[19]
  i808.debugStoreTargetId = !!i809[20]
  i808.showPreviewPanel = !!i809[21]
  i808.storeSettingsLocation = i809[22]
  i808.modules = request.d('DG.Tweening.Core.DOTweenSettings+ModulesSetup', i809[23], i808.modules)
  i808.createASMDEF = !!i809[24]
  i808.showPlayingTweens = !!i809[25]
  i808.showPausedTweens = !!i809[26]
  return i808
}

Deserializers["DG.Tweening.Core.DOTweenSettings+SafeModeOptions"] = function (request, data, root) {
  var i810 = root || request.c( 'DG.Tweening.Core.DOTweenSettings+SafeModeOptions' )
  var i811 = data
  i810.logBehaviour = i811[0]
  i810.nestedTweenFailureBehaviour = i811[1]
  return i810
}

Deserializers["DG.Tweening.Core.DOTweenSettings+ModulesSetup"] = function (request, data, root) {
  var i812 = root || request.c( 'DG.Tweening.Core.DOTweenSettings+ModulesSetup' )
  var i813 = data
  i812.showPanel = !!i813[0]
  i812.audioEnabled = !!i813[1]
  i812.physicsEnabled = !!i813[2]
  i812.physics2DEnabled = !!i813[3]
  i812.spriteEnabled = !!i813[4]
  i812.uiEnabled = !!i813[5]
  i812.textMeshProEnabled = !!i813[6]
  i812.tk2DEnabled = !!i813[7]
  i812.deAudioEnabled = !!i813[8]
  i812.deUnityExtendedEnabled = !!i813[9]
  i812.epoOutlineEnabled = !!i813[10]
  return i812
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Resources"] = function (request, data, root) {
  var i814 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Resources' )
  var i815 = data
  var i817 = i815[0]
  var i816 = []
  for(var i = 0; i < i817.length; i += 1) {
    i816.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Resources+File', i817[i + 0]) );
  }
  i814.files = i816
  i814.componentToPrefabIds = i815[1]
  return i814
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Resources+File"] = function (request, data, root) {
  var i820 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Resources+File' )
  var i821 = data
  i820.path = i821[0]
  request.r(i821[1], i821[2], 0, i820, 'unityObject')
  return i820
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings"] = function (request, data, root) {
  var i822 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings' )
  var i823 = data
  var i825 = i823[0]
  var i824 = []
  for(var i = 0; i < i825.length; i += 1) {
    i824.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder', i825[i + 0]) );
  }
  i822.scriptsExecutionOrder = i824
  var i827 = i823[1]
  var i826 = []
  for(var i = 0; i < i827.length; i += 1) {
    i826.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer', i827[i + 0]) );
  }
  i822.sortingLayers = i826
  var i829 = i823[2]
  var i828 = []
  for(var i = 0; i < i829.length; i += 1) {
    i828.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer', i829[i + 0]) );
  }
  i822.cullingLayers = i828
  i822.timeSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings', i823[3], i822.timeSettings)
  i822.physicsSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings', i823[4], i822.physicsSettings)
  i822.physics2DSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings', i823[5], i822.physics2DSettings)
  i822.qualitySettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.QualitySettings', i823[6], i822.qualitySettings)
  i822.enableRealtimeShadows = !!i823[7]
  i822.enableAutoInstancing = !!i823[8]
  i822.enableDynamicBatching = !!i823[9]
  i822.lightmapEncodingQuality = i823[10]
  i822.desiredColorSpace = i823[11]
  var i831 = i823[12]
  var i830 = []
  for(var i = 0; i < i831.length; i += 1) {
    i830.push( i831[i + 0] );
  }
  i822.allTags = i830
  return i822
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder"] = function (request, data, root) {
  var i834 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder' )
  var i835 = data
  i834.name = i835[0]
  i834.value = i835[1]
  return i834
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer"] = function (request, data, root) {
  var i838 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer' )
  var i839 = data
  i838.id = i839[0]
  i838.name = i839[1]
  i838.value = i839[2]
  return i838
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer"] = function (request, data, root) {
  var i842 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer' )
  var i843 = data
  i842.id = i843[0]
  i842.name = i843[1]
  return i842
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings"] = function (request, data, root) {
  var i844 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings' )
  var i845 = data
  i844.fixedDeltaTime = i845[0]
  i844.maximumDeltaTime = i845[1]
  i844.timeScale = i845[2]
  i844.maximumParticleTimestep = i845[3]
  return i844
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings"] = function (request, data, root) {
  var i846 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings' )
  var i847 = data
  i846.gravity = new pc.Vec3( i847[0], i847[1], i847[2] )
  i846.defaultSolverIterations = i847[3]
  i846.bounceThreshold = i847[4]
  i846.autoSyncTransforms = !!i847[5]
  i846.autoSimulation = !!i847[6]
  var i849 = i847[7]
  var i848 = []
  for(var i = 0; i < i849.length; i += 1) {
    i848.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask', i849[i + 0]) );
  }
  i846.collisionMatrix = i848
  return i846
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask"] = function (request, data, root) {
  var i852 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask' )
  var i853 = data
  i852.enabled = !!i853[0]
  i852.layerId = i853[1]
  i852.otherLayerId = i853[2]
  return i852
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings"] = function (request, data, root) {
  var i854 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings' )
  var i855 = data
  request.r(i855[0], i855[1], 0, i854, 'material')
  i854.gravity = new pc.Vec2( i855[2], i855[3] )
  i854.positionIterations = i855[4]
  i854.velocityIterations = i855[5]
  i854.velocityThreshold = i855[6]
  i854.maxLinearCorrection = i855[7]
  i854.maxAngularCorrection = i855[8]
  i854.maxTranslationSpeed = i855[9]
  i854.maxRotationSpeed = i855[10]
  i854.baumgarteScale = i855[11]
  i854.baumgarteTOIScale = i855[12]
  i854.timeToSleep = i855[13]
  i854.linearSleepTolerance = i855[14]
  i854.angularSleepTolerance = i855[15]
  i854.defaultContactOffset = i855[16]
  i854.autoSimulation = !!i855[17]
  i854.queriesHitTriggers = !!i855[18]
  i854.queriesStartInColliders = !!i855[19]
  i854.callbacksOnDisable = !!i855[20]
  i854.reuseCollisionCallbacks = !!i855[21]
  i854.autoSyncTransforms = !!i855[22]
  var i857 = i855[23]
  var i856 = []
  for(var i = 0; i < i857.length; i += 1) {
    i856.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask', i857[i + 0]) );
  }
  i854.collisionMatrix = i856
  return i854
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask"] = function (request, data, root) {
  var i860 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask' )
  var i861 = data
  i860.enabled = !!i861[0]
  i860.layerId = i861[1]
  i860.otherLayerId = i861[2]
  return i860
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.QualitySettings"] = function (request, data, root) {
  var i862 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.QualitySettings' )
  var i863 = data
  var i865 = i863[0]
  var i864 = []
  for(var i = 0; i < i865.length; i += 1) {
    i864.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.QualitySettings', i865[i + 0]) );
  }
  i862.qualityLevels = i864
  var i867 = i863[1]
  var i866 = []
  for(var i = 0; i < i867.length; i += 1) {
    i866.push( i867[i + 0] );
  }
  i862.names = i866
  i862.shadows = i863[2]
  i862.anisotropicFiltering = i863[3]
  i862.antiAliasing = i863[4]
  i862.lodBias = i863[5]
  i862.shadowCascades = i863[6]
  i862.shadowDistance = i863[7]
  i862.shadowmaskMode = i863[8]
  i862.shadowProjection = i863[9]
  i862.shadowResolution = i863[10]
  i862.softParticles = !!i863[11]
  i862.softVegetation = !!i863[12]
  i862.activeColorSpace = i863[13]
  i862.desiredColorSpace = i863[14]
  i862.masterTextureLimit = i863[15]
  i862.maxQueuedFrames = i863[16]
  i862.particleRaycastBudget = i863[17]
  i862.pixelLightCount = i863[18]
  i862.realtimeReflectionProbes = !!i863[19]
  i862.shadowCascade2Split = i863[20]
  i862.shadowCascade4Split = new pc.Vec3( i863[21], i863[22], i863[23] )
  i862.streamingMipmapsActive = !!i863[24]
  i862.vSyncCount = i863[25]
  i862.asyncUploadBufferSize = i863[26]
  i862.asyncUploadTimeSlice = i863[27]
  i862.billboardsFaceCameraPosition = !!i863[28]
  i862.shadowNearPlaneOffset = i863[29]
  i862.streamingMipmapsMemoryBudget = i863[30]
  i862.maximumLODLevel = i863[31]
  i862.streamingMipmapsAddAllCameras = !!i863[32]
  i862.streamingMipmapsMaxLevelReduction = i863[33]
  i862.streamingMipmapsRenderersPerFrame = i863[34]
  i862.resolutionScalingFixedDPIFactor = i863[35]
  i862.streamingMipmapsMaxFileIORequests = i863[36]
  i862.currentQualityLevel = i863[37]
  return i862
}

Deserializers["UnityEngine.Events.ArgumentCache"] = function (request, data, root) {
  var i870 = root || request.c( 'UnityEngine.Events.ArgumentCache' )
  var i871 = data
  request.r(i871[0], i871[1], 0, i870, 'm_ObjectArgument')
  i870.m_ObjectArgumentAssemblyTypeName = i871[2]
  i870.m_IntArgument = i871[3]
  i870.m_FloatArgument = i871[4]
  i870.m_StringArgument = i871[5]
  i870.m_BoolArgument = !!i871[6]
  return i870
}

Deserializers.fields = {"Luna.Unity.DTO.UnityEngine.Components.Transform":{"position":0,"scale":3,"rotation":6},"Luna.Unity.DTO.UnityEngine.Components.SpriteRenderer":{"enabled":0,"sharedMaterial":1,"sharedMaterials":3,"receiveShadows":4,"shadowCastingMode":5,"sortingLayerID":6,"sortingOrder":7,"lightmapIndex":8,"lightmapSceneIndex":9,"lightmapScaleOffset":10,"lightProbeUsage":14,"reflectionProbeUsage":15,"color":16,"sprite":20,"flipX":22,"flipY":23,"drawMode":24,"size":25,"tileMode":27,"adaptiveModeThreshold":28,"maskInteraction":29,"spriteSortPoint":30},"Luna.Unity.DTO.UnityEngine.Scene.GameObject":{"name":0,"tagId":1,"enabled":2,"isStatic":3,"layer":4},"Luna.Unity.DTO.UnityEngine.Assets.Material":{"name":0,"shader":1,"renderQueue":3,"enableInstancing":4,"floatParameters":5,"colorParameters":6,"vectorParameters":7,"textureParameters":8,"materialFlags":9},"Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag":{"name":0,"enabled":1},"Luna.Unity.DTO.UnityEngine.Components.BoxCollider2D":{"usedByComposite":0,"autoTiling":1,"size":2,"edgeRadius":4,"enabled":5,"isTrigger":6,"usedByEffector":7,"density":8,"offset":9,"material":11},"Luna.Unity.DTO.UnityEngine.Textures.Texture2D":{"name":0,"width":1,"height":2,"mipmapCount":3,"anisoLevel":4,"filterMode":5,"hdr":6,"format":7,"wrapMode":8,"alphaIsTransparency":9,"alphaSource":10,"graphicsFormat":11,"sRGBTexture":12,"desiredColorSpace":13,"wrapU":14,"wrapV":15},"Luna.Unity.DTO.UnityEngine.Components.RectTransform":{"pivot":0,"anchorMin":2,"anchorMax":4,"sizeDelta":6,"anchoredPosition3D":8,"rotation":11,"scale":15},"Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer":{"cullTransparentMesh":0},"Luna.Unity.DTO.UnityEngine.Components.CanvasGroup":{"m_Alpha":0,"m_Interactable":1,"m_BlocksRaycasts":2,"m_IgnoreParentGroups":3,"enabled":4},"Luna.Unity.DTO.UnityEngine.Scene.Scene":{"name":0,"index":1,"startup":2},"Luna.Unity.DTO.UnityEngine.Components.Camera":{"enabled":0,"aspect":1,"orthographic":2,"orthographicSize":3,"backgroundColor":4,"nearClipPlane":8,"farClipPlane":9,"fieldOfView":10,"depth":11,"clearFlags":12,"cullingMask":13,"rect":14,"targetTexture":15,"usePhysicalProperties":17,"focalLength":18,"sensorSize":19,"lensShift":21,"gateFit":23,"commandBufferCount":24,"cameraType":25},"Luna.Unity.DTO.UnityEngine.Components.Canvas":{"enabled":0,"planeDistance":1,"referencePixelsPerUnit":2,"isFallbackOverlay":3,"renderMode":4,"renderOrder":5,"sortingLayerName":6,"sortingOrder":7,"scaleFactor":8,"worldCamera":9,"overrideSorting":11,"pixelPerfect":12,"targetDisplay":13,"overridePixelPerfect":14},"Luna.Unity.DTO.UnityEngine.Components.ParticleSystem":{"main":0,"colorBySpeed":1,"colorOverLifetime":2,"emission":3,"rotationBySpeed":4,"rotationOverLifetime":5,"shape":6,"sizeBySpeed":7,"sizeOverLifetime":8,"textureSheetAnimation":9,"velocityOverLifetime":10,"noise":11,"inheritVelocity":12,"forceOverLifetime":13,"limitVelocityOverLifetime":14,"useAutoRandomSeed":15,"randomSeed":16},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.MainModule":{"duration":0,"loop":1,"prewarm":2,"startDelay":3,"startLifetime":4,"startSpeed":5,"startSize3D":6,"startSizeX":7,"startSizeY":8,"startSizeZ":9,"startRotation3D":10,"startRotationX":11,"startRotationY":12,"startRotationZ":13,"startColor":14,"gravityModifier":15,"simulationSpace":16,"customSimulationSpace":17,"simulationSpeed":19,"useUnscaledTime":20,"scalingMode":21,"playOnAwake":22,"maxParticles":23,"emitterVelocityMode":24,"stopAction":25},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve":{"mode":0,"curveMin":1,"curveMax":2,"curveMultiplier":3,"constantMin":4,"constantMax":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient":{"mode":0,"gradientMin":1,"gradientMax":2,"colorMin":3,"colorMax":7},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient":{"mode":0,"colorKeys":1,"alphaKeys":2},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorBySpeedModule":{"enabled":0,"color":1,"range":2},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey":{"color":0,"time":4},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey":{"alpha":0,"time":1},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorOverLifetimeModule":{"enabled":0,"color":1},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.EmissionModule":{"enabled":0,"rateOverTime":1,"rateOverDistance":2,"bursts":3},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Burst":{"count":0,"cycleCount":1,"minCount":2,"maxCount":3,"repeatInterval":4,"time":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationBySpeedModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4,"range":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ShapeModule":{"enabled":0,"shapeType":1,"randomDirectionAmount":2,"sphericalDirectionAmount":3,"randomPositionAmount":4,"alignToDirection":5,"radius":6,"radiusMode":7,"radiusSpread":8,"radiusSpeed":9,"radiusThickness":10,"angle":11,"length":12,"boxThickness":13,"meshShapeType":16,"mesh":17,"meshRenderer":19,"skinnedMeshRenderer":21,"useMeshMaterialIndex":23,"meshMaterialIndex":24,"useMeshColors":25,"normalOffset":26,"arc":27,"arcMode":28,"arcSpread":29,"arcSpeed":30,"donutRadius":31,"position":32,"rotation":35,"scale":38},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeBySpeedModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4,"range":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.TextureSheetAnimationModule":{"enabled":0,"mode":1,"animation":2,"numTilesX":3,"numTilesY":4,"useRandomRow":5,"frameOverTime":6,"startFrame":7,"cycleCount":8,"rowIndex":9,"flipU":10,"flipV":11,"spriteCount":12,"sprites":13},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.VelocityOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"radial":4,"speedModifier":5,"space":6,"orbitalX":7,"orbitalY":8,"orbitalZ":9,"orbitalOffsetX":10,"orbitalOffsetY":11,"orbitalOffsetZ":12},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.NoiseModule":{"enabled":0,"separateAxes":1,"strengthX":2,"strengthY":3,"strengthZ":4,"frequency":5,"damping":6,"octaveCount":7,"octaveMultiplier":8,"octaveScale":9,"quality":10,"scrollSpeed":11,"scrollSpeedMultiplier":12,"remapEnabled":13,"remapX":14,"remapY":15,"remapZ":16,"positionAmount":17,"rotationAmount":18,"sizeAmount":19},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.InheritVelocityModule":{"enabled":0,"mode":1,"curve":2},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ForceOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"space":4,"randomized":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.LimitVelocityOverLifetimeModule":{"enabled":0,"limit":1,"limitX":2,"limitY":3,"limitZ":4,"dampen":5,"separateAxes":6,"space":7,"drag":8,"multiplyDragByParticleSize":9,"multiplyDragByParticleVelocity":10},"Luna.Unity.DTO.UnityEngine.Components.ParticleSystemRenderer":{"enabled":0,"sharedMaterial":1,"sharedMaterials":3,"receiveShadows":4,"shadowCastingMode":5,"sortingLayerID":6,"sortingOrder":7,"lightmapIndex":8,"lightmapSceneIndex":9,"lightmapScaleOffset":10,"lightProbeUsage":14,"reflectionProbeUsage":15,"mesh":16,"meshCount":18,"activeVertexStreamsCount":19,"alignment":20,"renderMode":21,"sortMode":22,"lengthScale":23,"velocityScale":24,"cameraVelocityScale":25,"normalDirection":26,"sortingFudge":27,"minParticleSize":28,"maxParticleSize":29,"pivot":30,"trailMaterial":33},"Luna.Unity.DTO.UnityEngine.Components.AudioSource":{"clip":0,"outputAudioMixerGroup":2,"playOnAwake":4,"loop":5,"time":6,"volume":7,"pitch":8,"enabled":9},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings":{"ambientIntensity":0,"reflectionIntensity":1,"ambientMode":2,"ambientLight":3,"ambientSkyColor":7,"ambientGroundColor":11,"ambientEquatorColor":15,"fogColor":19,"fogEndDistance":23,"fogStartDistance":24,"fogDensity":25,"fog":26,"skybox":27,"fogMode":29,"lightmaps":30,"lightProbes":31,"lightmapsMode":32,"mixedBakeMode":33,"environmentLightingMode":34,"ambientProbe":35,"referenceAmbientProbe":36,"useReferenceAmbientProbe":37,"customReflection":38,"defaultReflection":40,"defaultReflectionMode":42,"defaultReflectionResolution":43,"sunLightObjectId":44,"pixelLightCount":45,"defaultReflectionHDR":46,"hasLightDataAsset":47,"hasManualGenerate":48},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap":{"lightmapColor":0,"lightmapDirection":2},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes":{"bakedProbes":0,"positions":1,"hullRays":2,"tetrahedra":3,"neighbours":4,"matrices":5},"Luna.Unity.DTO.UnityEngine.Assets.Shader":{"ShaderCompilationErrors":0,"name":1,"guid":2,"shaderDefinedKeywords":3,"passes":4,"usePasses":5,"defaultParameterValues":6,"unityFallbackShader":7,"readDepth":9,"isCreatedByShaderGraph":10,"compiled":11},"Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError":{"shaderName":0,"errorMessage":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass":{"id":0,"subShaderIndex":1,"name":2,"passType":3,"grabPassTextureName":4,"usePass":5,"zTest":6,"zWrite":7,"culling":8,"blending":9,"alphaBlending":10,"colorWriteMask":11,"offsetUnits":12,"offsetFactor":13,"stencilRef":14,"stencilReadMask":15,"stencilWriteMask":16,"stencilOp":17,"stencilOpFront":18,"stencilOpBack":19,"tags":20,"passDefinedKeywords":21,"passDefinedKeywordGroups":22,"variants":23,"excludedVariants":24,"hasDepthReader":25},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value":{"val":0,"name":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending":{"src":0,"dst":1,"op":2},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp":{"pass":0,"fail":1,"zFail":2,"comp":3},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup":{"keywords":0,"hasDiscard":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant":{"passId":0,"subShaderIndex":1,"keywords":2,"vertexProgram":3,"fragmentProgram":4,"exportedForWebGl2":5,"readDepth":6},"Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass":{"shader":0,"pass":2},"Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue":{"name":0,"type":1,"value":2,"textureValue":6,"shaderPropertyFlag":7},"Luna.Unity.DTO.UnityEngine.Textures.Sprite":{"name":0,"texture":1,"aabb":3,"vertices":4,"triangles":5,"textureRect":6,"packedRect":10,"border":14,"transparency":18,"bounds":19,"pixelsPerUnit":20,"textureWidth":21,"textureHeight":22,"nativeSize":23,"pivot":25,"textureRectOffset":27},"Luna.Unity.DTO.UnityEngine.Assets.AudioClip":{"name":0},"Luna.Unity.DTO.UnityEngine.Assets.Font":{"name":0,"ascent":1,"originalLineHeight":2,"fontSize":3,"characterInfo":4,"texture":5,"originalFontSize":7},"Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo":{"index":0,"advance":1,"bearing":2,"glyphWidth":3,"glyphHeight":4,"minX":5,"maxX":6,"minY":7,"maxY":8,"uvBottomLeftX":9,"uvBottomLeftY":10,"uvBottomRightX":11,"uvBottomRightY":12,"uvTopLeftX":13,"uvTopLeftY":14,"uvTopRightX":15,"uvTopRightY":16},"Luna.Unity.DTO.UnityEngine.Assets.TextAsset":{"name":0,"bytes64":1,"data":2},"Luna.Unity.DTO.UnityEngine.Assets.Resources":{"files":0,"componentToPrefabIds":1},"Luna.Unity.DTO.UnityEngine.Assets.Resources+File":{"path":0,"unityObject":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings":{"scriptsExecutionOrder":0,"sortingLayers":1,"cullingLayers":2,"timeSettings":3,"physicsSettings":4,"physics2DSettings":5,"qualitySettings":6,"enableRealtimeShadows":7,"enableAutoInstancing":8,"enableDynamicBatching":9,"lightmapEncodingQuality":10,"desiredColorSpace":11,"allTags":12},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer":{"id":0,"name":1,"value":2},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer":{"id":0,"name":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings":{"fixedDeltaTime":0,"maximumDeltaTime":1,"timeScale":2,"maximumParticleTimestep":3},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings":{"gravity":0,"defaultSolverIterations":3,"bounceThreshold":4,"autoSyncTransforms":5,"autoSimulation":6,"collisionMatrix":7},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask":{"enabled":0,"layerId":1,"otherLayerId":2},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings":{"material":0,"gravity":2,"positionIterations":4,"velocityIterations":5,"velocityThreshold":6,"maxLinearCorrection":7,"maxAngularCorrection":8,"maxTranslationSpeed":9,"maxRotationSpeed":10,"baumgarteScale":11,"baumgarteTOIScale":12,"timeToSleep":13,"linearSleepTolerance":14,"angularSleepTolerance":15,"defaultContactOffset":16,"autoSimulation":17,"queriesHitTriggers":18,"queriesStartInColliders":19,"callbacksOnDisable":20,"reuseCollisionCallbacks":21,"autoSyncTransforms":22,"collisionMatrix":23},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask":{"enabled":0,"layerId":1,"otherLayerId":2},"Luna.Unity.DTO.UnityEngine.Assets.QualitySettings":{"qualityLevels":0,"names":1,"shadows":2,"anisotropicFiltering":3,"antiAliasing":4,"lodBias":5,"shadowCascades":6,"shadowDistance":7,"shadowmaskMode":8,"shadowProjection":9,"shadowResolution":10,"softParticles":11,"softVegetation":12,"activeColorSpace":13,"desiredColorSpace":14,"masterTextureLimit":15,"maxQueuedFrames":16,"particleRaycastBudget":17,"pixelLightCount":18,"realtimeReflectionProbes":19,"shadowCascade2Split":20,"shadowCascade4Split":21,"streamingMipmapsActive":24,"vSyncCount":25,"asyncUploadBufferSize":26,"asyncUploadTimeSlice":27,"billboardsFaceCameraPosition":28,"shadowNearPlaneOffset":29,"streamingMipmapsMemoryBudget":30,"maximumLODLevel":31,"streamingMipmapsAddAllCameras":32,"streamingMipmapsMaxLevelReduction":33,"streamingMipmapsRenderersPerFrame":34,"resolutionScalingFixedDPIFactor":35,"streamingMipmapsMaxFileIORequests":36,"currentQualityLevel":37}}

Deserializers.requiredComponents = {"65":[66],"67":[66],"68":[66],"69":[66],"70":[66],"71":[66],"72":[73],"74":[25],"75":[76],"77":[76],"78":[76],"79":[76],"80":[76],"81":[76],"82":[76],"83":[84],"85":[84],"86":[84],"87":[84],"88":[84],"89":[84],"90":[84],"91":[84],"92":[84],"93":[84],"94":[84],"95":[84],"96":[84],"97":[25],"98":[99],"100":[101],"102":[101],"33":[9],"29":[1],"5":[1],"41":[39,40],"103":[104],"105":[1],"106":[104],"107":[9],"108":[9],"35":[33],"13":[10,9],"109":[9],"34":[33],"110":[9],"111":[9],"112":[9],"113":[9],"114":[9],"115":[9],"116":[9],"117":[9],"118":[9],"119":[10,9],"120":[9],"121":[9],"122":[9],"23":[9],"22":[10,9],"123":[9],"124":[45],"125":[45],"46":[45],"126":[45],"127":[25],"128":[25],"129":[130],"131":[25],"132":[104]}

Deserializers.types = ["UnityEngine.Transform","UnityEngine.SpriteRenderer","UnityEngine.Material","UnityEngine.Sprite","UnityEngine.MonoBehaviour","SpriteCornerDebugger","PerfectLineSettingSO","UnityEngine.Shader","UnityEngine.BoxCollider2D","UnityEngine.RectTransform","UnityEngine.CanvasRenderer","UnityEngine.CanvasGroup","UnityEngine.EventSystems.UIBehaviour","UnityEngine.UI.Image","ECS_MagicTile.GlobalPoint","ECS_MagicTile.GeneralGameSetting","ECS_MagicTile.MusicNoteCreationSetting","ECS_MagicTile.PerfectLineSetting","ECS_MagicTile.LaneLineSettings","EventChannel.IntEventChannel","EventChannel.BoolEventChannel","EventChannel.EmptyEventChannel","UnityEngine.UI.Text","UnityEngine.UI.Slider","UnityEngine.GameObject","UnityEngine.Camera","ECS_MagicTile.GameIntroSystem","UnityEngine.AudioListener","PerfectLineCameraSpacePositionAdjuster","PerfectLineSpriteResizer","PerfectLineFakeVisual","ECS_MagicTile.RaycastToStartGame","UnityEngine.UI.Button","UnityEngine.Canvas","UnityEngine.UI.CanvasScaler","UnityEngine.UI.GraphicRaycaster","UnityEngine.Font","ECS_MagicTile.ScoreEffectController","BurstMovementUIController","ECS_MagicTile.StarTween","ECS_MagicTile.CrownTween","ECS_MagicTile.ProgressEffectController","UnityEngine.ParticleSystem","UnityEngine.ParticleSystemRenderer","ECS_MagicTile.EffectOnProgress","UnityEngine.EventSystems.EventSystem","UnityEngine.EventSystems.StandaloneInputModule","ECS_MagicTile.ScreenManager","UnityEngine.AudioSource","ECS_MagicTile.AudioManager","UnityEngine.AudioClip","Facade.Tweening.TweenManager","GlobalGameSetting","GeneralGameSettingSO","DataSystemSettingSO","PresenterSettingSO","MusicNoteSettingSO","LaneLineSettingSO","IntroNoteSettingSO","GizmoDebugger","ManualDebug","MusicTileManager","UnityEngine.Texture2D","UnityEngine.TextAsset","DG.Tweening.Core.DOTweenSettings","UnityEngine.AudioLowPassFilter","UnityEngine.AudioBehaviour","UnityEngine.AudioHighPassFilter","UnityEngine.AudioReverbFilter","UnityEngine.AudioDistortionFilter","UnityEngine.AudioEchoFilter","UnityEngine.AudioChorusFilter","UnityEngine.Cloth","UnityEngine.SkinnedMeshRenderer","UnityEngine.FlareLayer","UnityEngine.ConstantForce","UnityEngine.Rigidbody","UnityEngine.Joint","UnityEngine.HingeJoint","UnityEngine.SpringJoint","UnityEngine.FixedJoint","UnityEngine.CharacterJoint","UnityEngine.ConfigurableJoint","UnityEngine.CompositeCollider2D","UnityEngine.Rigidbody2D","UnityEngine.Joint2D","UnityEngine.AnchoredJoint2D","UnityEngine.SpringJoint2D","UnityEngine.DistanceJoint2D","UnityEngine.FrictionJoint2D","UnityEngine.HingeJoint2D","UnityEngine.RelativeJoint2D","UnityEngine.SliderJoint2D","UnityEngine.TargetJoint2D","UnityEngine.FixedJoint2D","UnityEngine.WheelJoint2D","UnityEngine.ConstantForce2D","UnityEngine.StreamingController","UnityEngine.TextMesh","UnityEngine.MeshRenderer","UnityEngine.Tilemaps.TilemapRenderer","UnityEngine.Tilemaps.Tilemap","UnityEngine.Tilemaps.TilemapCollider2D","Unity.VisualScripting.SceneVariables","Unity.VisualScripting.Variables","UnityEngine.U2D.Animation.SpriteSkin","Unity.VisualScripting.ScriptMachine","UnityEngine.UI.Dropdown","UnityEngine.UI.Graphic","UnityEngine.UI.AspectRatioFitter","UnityEngine.UI.ContentSizeFitter","UnityEngine.UI.GridLayoutGroup","UnityEngine.UI.HorizontalLayoutGroup","UnityEngine.UI.HorizontalOrVerticalLayoutGroup","UnityEngine.UI.LayoutElement","UnityEngine.UI.LayoutGroup","UnityEngine.UI.VerticalLayoutGroup","UnityEngine.UI.Mask","UnityEngine.UI.MaskableGraphic","UnityEngine.UI.RawImage","UnityEngine.UI.RectMask2D","UnityEngine.UI.Scrollbar","UnityEngine.UI.ScrollRect","UnityEngine.UI.Toggle","UnityEngine.EventSystems.BaseInputModule","UnityEngine.EventSystems.PointerInputModule","UnityEngine.EventSystems.TouchInputModule","UnityEngine.EventSystems.Physics2DRaycaster","UnityEngine.EventSystems.PhysicsRaycaster","UnityEngine.U2D.SpriteShapeController","UnityEngine.U2D.SpriteShapeRenderer","UnityEngine.U2D.PixelPerfectCamera","Unity.VisualScripting.StateMachine"]

Deserializers.unityVersion = "2022.3.58f1";

Deserializers.productName = "DOD-Project";

Deserializers.lunaInitializationTime = "02/20/2025 09:40:26";

Deserializers.lunaDaysRunning = "8.1";

Deserializers.lunaVersion = "6.2.0";

Deserializers.lunaSHA = "7963e9fed253d218ae1c5298f104efd7e457ea14";

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

Deserializers.buildID = "95466a5b-4047-455d-ae73-73c1986d8f34";

Deserializers.runtimeInitializeOnLoadInfos = [[["UnityEngine","Experimental","Rendering","ScriptableRuntimeReflectionSystemSettings","ScriptingDirtyReflectionSystemInstance"]],[["Unity","VisualScripting","RuntimeVSUsageUtility","RuntimeInitializeOnLoadBeforeSceneLoad"]],[["$BurstDirectCallInitializer","Initialize"],["$BurstDirectCallInitializer","Initialize"],["$BurstDirectCallInitializer","Initialize"],["$BurstDirectCallInitializer","Initialize"],["$BurstDirectCallInitializer","Initialize"],["$BurstDirectCallInitializer","Initialize"],["$BurstDirectCallInitializer","Initialize"]],[],[]];

Deserializers.typeNameToIdMap = function(){ var i = 0; return Deserializers.types.reduce( function( res, item ) { res[ item ] = i++; return res; }, {} ) }()

