using System;
using System.Collections.Generic;

public struct NodesRequest
{
	public Predicate<GridBaseNode> Filter;
	public Action<List<GridBaseNode>> Callback;

    public NodesRequest(Predicate<GridBaseNode> filter, Action<List<GridBaseNode>> callback)
    {
		Filter = filter;
		Callback = callback;
    }
}