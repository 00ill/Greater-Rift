using System;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingManager
{
    struct PathRequest
    {
        public readonly Vector3 PathStart;
        public readonly Vector3 PathEnd;
        public readonly Action<Vector3[], bool> Callback;

        public PathRequest(Vector3 start, Vector3 end, Action<Vector3[], bool> callback)
        {
            PathStart = start;
            PathEnd = end;
            Callback = callback;
        }
    }


    private readonly Queue<PathRequest> _pathRequestQueue = new();
    private PathRequest _currentPathRequest;
    private Pathfinding _pathfinding;

    private bool _isProcessingPath;

    public void Init()
    {
        _pathfinding = GameObject.FindObjectOfType<Pathfinding>();
        //패스파인딩 초기화
    }

    public void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        var newRequest = new PathRequest(pathStart, pathEnd, callback);
        _pathRequestQueue.Enqueue(newRequest);
    }

    void TryProcessNext()
    {
        if (!_isProcessingPath && _pathRequestQueue.Count > 0)
        {
            _currentPathRequest = _pathRequestQueue.Dequeue();
            _isProcessingPath = true;
            _pathfinding.StartFindPath(_currentPathRequest.PathStart, _currentPathRequest.PathEnd);
        }
    }
    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        _currentPathRequest.Callback(path, success);
        _isProcessingPath = false;
        TryProcessNext();
    }
}


