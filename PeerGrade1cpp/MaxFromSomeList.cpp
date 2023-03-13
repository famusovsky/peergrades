#include "MaxFromSomeList.h"

int MaxFromEdgeList(const std::map<std::pair<int, int>, int>& map) {
  int max = -1;
  for (auto i : map) {
    if (i.first.first > max) {
      max = i.first.first;
    }
    if (i.first.second > max) {
      max = i.first.second;
    }
  }
  return max;
}

int MaxFromAdjacencyList(const std::map<int, std::set<int>>& map) {
  int max = 0;
  for (const auto& pair : map) {
    if (pair.first > max) {
      max = pair.first;
    }
    for (auto it : pair.second) {
      if (it > max) {
        max = it;
      }
    }
  }
  return max;
}
