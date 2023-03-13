#include "EdgeList.h"
#include "MaxFromSomeList.h"
#include <vector>
#include <stack>
#include <map>
#include <string>
#include <iostream>
#include <sstream>
#include <algorithm>

EdgeList::EdgeList(std::istream & istream) : Graph(istream) {
  while (true) {
    std::string inp;
    std::getline(istream, inp);
    std::istringstream stringstream(inp);
    if (inp != "End") {
      int num;
      std::vector<int> vec;
      std::pair<int, int> pair;
      while (stringstream >> num) {
        vec.push_back(num);
      }
      pair.first = vec[0];
      pair.second = vec[1];
      map[pair] = vec[2];
      if (!isDirected) {
        pair.second = vec[0];
        pair.first = vec[1];
        map[pair] = vec[2];
      }
      if (istream.eof()) {
        break;
      }
    } else {
      break;
    }
  }
}

void EdgeList::PrintAsEdgeList(std::ostream &ostream) {
  std::ostringstream stringstream;
  stringstream << "Список рёбер (старт - финиш - вес)\n";
  for (auto i : map) {
    stringstream << "---------\n" << i.first.first << " | " << i.first.second << " | " << i.second
                 << '\n';
  }
  ostream << stringstream.str();
}

void EdgeList::PrintAsAdjacencyList(std::ostream &ostream) {
  std::ostringstream stringstream;
  stringstream << "Список смежности данного графа:\n";
  std::map<int, std::stack<int>> map_c;
  for (auto item : map) {
    if (!map_c.contains(item.first.first)) {
      std::stack<int> stack;
      map_c[item.first.first] = stack;
    }
    map_c[item.first.first].push(item.first.second);
  }
  for (auto item : map_c) {
    stringstream << "Вершина " << item.first << " соединена с :\n";
    while (!item.second.empty()) {
      stringstream << item.second.top() << ' ';
      item.second.pop();
    }
    stringstream << '\n';
  }
  ostream << stringstream.str();
}

void EdgeList::PrintAsAdjacencyMatrix(std::ostream &ostream) {
  std::ostringstream stringstream;
  stringstream << ' ';
  int max = MaxFromEdgeList(map);
  for (int i = 1 ; i <= max ; ++i) {
    stringstream << "  " << i;
  }
  for (int i = 1 ; i <= max ; ++i) {
    stringstream << "\n" << i;
    std::pair<int, int> pair;
    for (int j = 1 ; j <= max ; ++j) {
      pair.first = i;
      pair.second = j;
      bool isContaining = map.contains(pair);
      std::swap(pair.first, pair.second);
      isContaining = isContaining || map.contains(pair);
      stringstream << "  " << (isContaining ? 1 : 0);
    }
  }
  stringstream << '\n';
  ostream << stringstream.str();
}

void EdgeList::PrintAsIncidenceMatrix(std::ostream &ostream) {
  std::ostringstream stringstream;
  int max = MaxFromEdgeList(map);
  stringstream << ' ';
  for (int i = 1 ; i <= max ; ++i) {
    stringstream << "  " << i;
  }
  int i = 1;
  for (auto &it : map) {
    stringstream << "\n" << i++;
    for (int j = 1 ; j <= max ; ++j) {
      int isContaining = 0;
      if (it.first.second == j) {
        isContaining = 1;
      } else if (it.first.first == j) {
        isContaining = isDirected ? -1 : 1;
      }
      stringstream << (isContaining == -1 ? " " : "  ") << isContaining;
    }
  }
  stringstream << '\n';
  ostream << stringstream.str();
}

void EdgeList::DegreesCount(std::ostream &ostream) {
  int cnt = 0;
  std::ostringstream stringstream;
  stringstream << "верш. " << (isDirected ? "полу" : "") << "ст.\n";
  for (int i = 1 ; i < MaxFromEdgeList(map) ; ++i) {
    stringstream << ' ' << i << " --- ";
    for (auto it : map) {
      if (it.first.first == i) {
        cnt++;
      }
    }
    stringstream << cnt << '\n';
    cnt = 0;
  }
  ostream << stringstream.str();
}

void EdgeList::EdgesCount(std::ostream &ostream) {
  std::ostringstream stringstream;
  stringstream << "Число " << (isDirected ? "дуг = " : "рёбер = ")
               << (isDirected ? map.size() : static_cast<int>(map.size() / 2));
  ostream << stringstream.str();
}
