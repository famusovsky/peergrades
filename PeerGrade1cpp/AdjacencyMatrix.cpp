#include <vector>
#include "AdjacencyMatrix.h"
#include "MatrixInput.h"
#include <vector>
#include <map>
#include <set>
#include <iostream>
#include <fstream>
#include <sstream>
#include <string>
#include <stack>
#include <map>

AdjacencyMatrix::AdjacencyMatrix(std::istream &istream) : Graph(istream) {
  matrix = MatrixInput(istream);
}

void AdjacencyMatrix::PrintAsEdgeList(std::ostream &ostream) {
  std::ostringstream stringstream;
  stringstream << "Список рёбер (старт - финиш)\n";
  std::map<int, std::set<int>> map;
  for (int i = 0 ; i < static_cast<int>(matrix.size()) ; ++i) {
    for (int j = 0 ; j < static_cast<int>(matrix[i].size()) ; ++j) {
      if (matrix[i][j] == 1) {
        if (!map.contains(i + 1)) {
          std::set<int> set;
          map[i + 1] = set;
        }
        if (!(!isDirected && map.contains(j + 1) && map[j + 1].contains(i + 1))) {
          map[i + 1].insert(j + 1);
        }
      }
    }
  }
  for (const auto &it : map) {
    for (const auto &jt : it.second) {
      stringstream << "-----\n" << it.first << " | " << jt << '\n';
    }
  }
  ostream << stringstream.str();
}

void AdjacencyMatrix::PrintAsAdjacencyList(std::ostream &ostream) {
  std::ostringstream stringstream;
  stringstream << "Список смежности данного графа:\n";
  for (int i = 0 ; i < static_cast<int>(matrix.size()) ; ++i) {
    std::vector<int> vec;
    for (int j = 0 ; j < static_cast<int>(matrix.size()) ; ++j) {
      if (matrix[i][j] == 1) {
        vec.push_back(j + 1);
      }
    }
    if (!vec.empty()) {
      stringstream << "Вершина " << i + 1 << " соединена с:";
      for (auto it : vec) {
        stringstream << ' ' << it;
      }
      stringstream << '\n';
    }
  }
  ostream << stringstream.str();
}

void AdjacencyMatrix::PrintAsAdjacencyMatrix(std::ostream &ostream) {
  std::ostringstream stringstream;
  stringstream << ' ';
  int max = static_cast<int>(matrix.size());
  for (int i = 1 ; i <= max ; ++i) {
    stringstream << "  " << i;
  }
  for (int i = 0 ; i < max ; ++i) {
    stringstream << "\n" << i + 1;
    for (int j = 0 ; j < max ; ++j) {
      stringstream << "  " << matrix[i][j];
    }
  }
  stringstream << '\n';
  ostream << stringstream.str();
}

void AdjacencyMatrix::PrintAsIncidenceMatrix(std::ostream &ostream) {
  std::ostringstream stringstream;
  int max = static_cast<int>(matrix.size());
  stringstream << ' ';
  for (int i = 1 ; i <= max ; ++i) {
    stringstream << "  " << i;
  }
  int i = 1;
  for (int j = 0 ; j < max ; ++j) {
    for (int k = 0 ; k < max ; ++k) {
      if (matrix[j][k] == 1 && !(!isDirected && matrix[k][j] == 1 && k < j)) {
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

void AdjacencyMatrix::DegreesCount(std::ostream &ostream) {
  int cnt = 0;
  std::ostringstream stringstream;
  stringstream << "верш. " << (isDirected ? "полу" : "") << "ст.\n";
  for (int i = 0 ; i < static_cast<int>(matrix.size()) ; ++i) {
    stringstream << ' ' << i + 1 << " --- ";
    for (auto it : matrix[i]) {
      if (it == 1) {
        cnt++;
      }
    }
    stringstream << cnt << '\n';
    cnt = 0;
  }
  ostream << stringstream.str();
}

void AdjacencyMatrix::EdgesCount(std::ostream &ostream) {
  std::ostringstream stringstream;
  int cnt = 0;
  for (auto &i : matrix) {
    for (auto it : i) {
      if (it == 1) {
        cnt++;
      }
    }
  }
  stringstream << "Число " << (isDirected ? "дуг = " : "рёбер = ")
               << (isDirected ? cnt : cnt / 2) << '\n';
  ostream << stringstream.str();
}
