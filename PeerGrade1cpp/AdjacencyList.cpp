#include <string>
#include <iostream>
#include "AdjacencyList.h"
#include "MaxFromSomeList.h"
#include <sstream>

AdjacencyList::AdjacencyList(std::istream &istream) : Graph(istream) {
  while (true) {
    std::string inp;
    std::getline(istream, inp);
    std::istringstream stringstream(inp);
    if (inp != "End") {
      int first, num;
      std::set<int> set;
      stringstream >> first;
      map[first] = set;
      while (stringstream >> num) {
        map[first].insert(num);
      }
      if (istream.eof()) {
        break;
      }
    } else {
      break;
    }
  }
}

void AdjacencyList::PrintAsEdgeList(std::ostream &ostream) {
  std::ostringstream stringstream;
  stringstream << "Список рёбер (старт - финиш)\n";
  for (const auto &pair : map) {
    for (auto it : pair.second) {
      if (isDirected || !isDirected && pair.first <= it) {
        stringstream << "-----\n" << pair.first << " | " << it << '\n';
      }
    }
  }
  ostream << stringstream.str();
}

void AdjacencyList::PrintAsAdjacencyList(std::ostream &ostream) {
  std::ostringstream stringstream;
  stringstream << "Список смежности данного графа:\n";
  for (const auto &pair : map) {
    if (!pair.second.empty()) {
      stringstream << "Вершина " << pair.first << " соединена с: ";
      for (auto it : pair.second) {
        stringstream << it << ' ';
      }
      stringstream << '\n';
    }
  }
  ostream << stringstream.str();
}

void AdjacencyList::PrintAsAdjacencyMatrix(std::ostream &ostream) {
  std::ostringstream stringstream;
  stringstream << ' ';
  int max = MaxFromAdjacencyList(map);
  for (int i = 1 ; i <= max ; ++i) {
    stringstream << "  " << i;
  }
  for (int i = 1 ; i <= max ; ++i) {
    stringstream << "\n" << i;
    for (int j = 0 ; j < max ; ++j) {
      bool isEdged = false;
      if (map.contains(i) && map[i].contains(j + 1)) {
        isEdged = true;
      }
      stringstream << "  " << (isEdged ? 1 : 0);
    }
  }
  stringstream << '\n';
  ostream << stringstream.str();
}

void AdjacencyList::PrintAsIncidenceMatrix(std::ostream &ostream) {
  std::ostringstream stringstream;
  int max = MaxFromAdjacencyList(map);
  stringstream << ' ';
  for (int i = 1 ; i <= max ; ++i) {
    stringstream << "  " << i;
  }
  int i = 1;
  for (int j = 0 ; j < max ; ++j) {
    for (int k = 0 ; k < max ; ++k) {
      if (map.contains(j) && map[j].contains(k)
          && !(!isDirected && map.contains(k) && map[k].contains(j) && k < j)) {
        stringstream << "\n" << i++;
        for (int z = 0 ; z < max ; ++z) {
          stringstream << (z == j ? isDirected ? " -1" : "  1" : z == k ? "  1" : "  0");
        }
      }
    }
  }
  stringstream << '\n';
  ostream << stringstream.str();
}

void AdjacencyList::DegreesCount(std::ostream &ostream) {
  std::ostringstream stringstream;
  stringstream << "верш. " << (isDirected ? "полу" : "") << "ст.\n";
  int max = MaxFromAdjacencyList(map);
  for (int i = 0 ; i < max ; ++i) {
    stringstream << ' ' << i + 1 << " --- "
                 << (map.contains(i + 1) ? static_cast<int>(map[i + 1].size()) : 0) << '\n';
  }
  ostream << stringstream.str();
}

void AdjacencyList::EdgesCount(std::ostream &ostream) {
  std::ostringstream stringstream;
  int cnt = 0;
  for (const auto &pair : map) {
    cnt += static_cast<int>(pair.second.size());
  }
  if (!isDirected) {
    cnt /= 2;
  }
  stringstream << "Число " << (isDirected ? "дуг = " : "рёбер = ")
               << cnt << '\n';
  ostream << stringstream.str();
}
