#ifndef PEERGRADE1CPP__MAXFROMSOMELIST_H_
#define PEERGRADE1CPP__MAXFROMSOMELIST_H_
#include <map>
#include <set>

// �������, ������������ ������������ ����� ������� �� ������ ����.
int MaxFromEdgeList(const std::map<std::pair<int, int>, int>& map);

// �������, ������������ ������������ ����� ������� �� ������ ���������.
int MaxFromAdjacencyList(const std::map<int, std::set<int>>& map);

#endif //PEERGRADE1CPP__MAXFROMSOMELIST_H_
