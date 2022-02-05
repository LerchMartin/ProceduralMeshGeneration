using ProceduralMeshes.Generators;
using ProceduralMeshes.Streams;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace ProceduralMeshes.Jobs
{
    [BurstCompile(FloatPrecision.Standard, FloatMode.Fast, CompileSynchronously = true)]            
    public struct MeshJob<G, S> : IJobFor
        where G : struct, IMeshGenerator
        where S : struct, IMeshStream
    {
        G generator;

        [WriteOnly]
        S streams;

        public void Execute(int index)
        {
            generator.Execute(index, streams);
        }

        public static JobHandle ScheduleParallel(Mesh mesh, Mesh.MeshData meshData, int resolution, JobHandle dependency)
        {
            MeshJob<G, S> job = new MeshJob<G, S>();

            job.generator.Resolution = resolution;

            job.streams.Initialize(
                meshData,
                job.generator.VertexCount, 
                job.generator.IndexCount, 
                mesh.bounds = job.generator.Bounds
            );

            return job.ScheduleParallel(job.generator.JobLength, 1, dependency);
        }
    }
}
