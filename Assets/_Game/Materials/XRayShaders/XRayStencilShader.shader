Shader "XRayStencilShader"
{
    Properties
    {
        //setup property var called "_StencilID" to be able to create lots of mat with diff IDs
        [IntRange] _StencilID ("Stencil ID", Range(0, 255)) = 0 
    }

     SubShader
     {
        Tags //write stencil value
        {
            "RenderType" = "Opaque"
            "Queue" = "Geometry" //run stencil mask shader before opaque geometry, using URP feats to force other obj ro render after this one
            "RenderPipeline" = "UniversalPipeline"
        }

        Pass
        {
            Blend Zero One //make stencil obj invisible; blend zero one takes color pixel then add zwrite off
            ZWrite Off //prevent shader writing to depth buffer

            Stencil //add stencil block function and put stencil logic inside
            {
                Ref [_StencilID] //get stencil id

                ///*add stencil test, compares ref value with whatever stencil value is already set on this pixel
                //*we always want to override the stencil value */
                Comp Always //stencil test always passes/runs
                
                ///*tell unity what to do when stencil test passes or fails
                 //*if pass - replace stencil value with new one we defined on this shader
                 //*if fail - keep whatever value was already in the stencil buffer for this pixel */
                 Pass Replace
                 Fail Keep
            }
        }
    }
}
