#include <vector>
#include <iostream>
#include <sstream>
#include <string>
#include <stack>
#include <map>
#include "IncidenceMatrix.h"
#include "MatrixInput.h"

IncidenceMatrix::IncidenceMatrix(std::istream &istream) : Graph(istream) {
  matrix = MatrixInput(istream);
}

void IncidenceMatrix::PrintAsEdgeList(std::ostream &ostream) {
  std::ostringstream stringstream;
  stringstream << "Список рёбер (старт - финиш)\n";
  for (auto vec : matrix) {
    std::pair<int, int> pair;
    if (isDirected) {
      pair.first = ++std::find(vec.begin(), vec.end(), -1) - vec.begin();
      pair.second = ++std::find(vec.begin(), vec.end(), 1) - vec.begin();
    } else {
      pair.first = ++std::find(vec.begin(), vec.end(), 1) - vec.begin();
      pair.second = ++std::find(vec.begin() + pair.first, vec.end(), 1) - vec.begin();
    }
    stringstream << "-----\n" << pair.first << " | " << pair.second << '\n';
  }
  ostream << stringstream.str();
}

void IncidenceMatrix::PrintAsAdjacencyList(std::ostream &ostream) {
  std::ostringstream stringstream;
  stringstream << "Список смежности данного графа:\n";
  std::map<int, std::stack<int>> map_c;
  for (auto vec : matrix) {
    int first, second;
    if (isDirected) {
      first = ++std::find(vec.begin(), vec.end(), -1) - vec.begin();
      second = ++std::find(vec.begin(), vec.end(), 1) - vec.begin();
    } else {
      first = ++std::find(vec.begin(), vec.end(), 1) - vec.begin();
      second = ++std::find(vec.begin() + first, vec.end(), 1) - vec.begin();
    }
    if (!map_c.contains(first)) {
      std::stack<int> stack;
      map_c[first] = stack;
    }
    map_c[first].push(second);
  }
  for (auto item : map_c) {
    stringstream << "Вершина " << item.first << " соединена с: ";
    while (!item.second.empty()) {
      stringstream << item.second.top() << ' ';
      item.second.pop();
    }
    stringstream << '\n';
  }
  ostream << stringstream.str();
}

void IncidenceMatrix::PrintAsAdjacencyMatrix(std::ostream &ostream) {
  std::ostringstream stringstream;
  stringstream << ' ';
  int max = static_cast<int>(matrix[0].size());
  for (int i = 1 ; i <= max ; ++i) {
    stringstream << "  " << i;
  }
  for (int i = 1 ; i <= max ; ++i) {
    stringstream << "\n" << i;
    for (int j = 0 ; j < max ; ++j) {
      bool isEdged = false;
      for (auto vec : matrix) {
        if ((isDirected && vec[i - 1] == -1 && vec[j] == 1)
            || (!isDirected && vec[i - 1] == vec[j] && vec[j] == 1) && j != i - 1) {
          isEdged = true;
          break;
        }
      }
      stringstream << "  " << (isEdged ? 1 : 0);
    }
  }
  stringstream << '\n';
  ostream << stringstream.str();
}

void IncidenceMatrix::PrintAsIncidenceMatrix(std::ostream &ostream) {
  std::ostringstream stringstream;
  int max = static_cast<int>(matrix[0].size());
  stringstream << ' ';
  for (int i = 1 ; i <= max ; ++i) {
    stringstream << "  " << i;
  }
  int i = 1;
  for (auto vec : matrix) {
    stringstream << "\n" << i++;
    for (int j = 0 ; j < max ; ++j) {
      stringstream << (vec[j] == -1 ? " " : "  ") << vec[j];
    }
  }
  stringstream << '\n';
  ostream << stringstream.str();
}

void IncidenceMatrix::DegreesCount(std::ostream &ostream) {
  int cnt = 0;
  std::ostringstream stringstream;
  stringstream << "верш. " << (isDirected ? "полу" : "") << "ст.\n";
  for (int i = 0 ; i < static_cast<int>(matrix[0].size()) ; ++i) {
    stringstream << ' ' << i + 1 << " --- ";
    for (auto vec : matrix) {
      if (isDirected && vec[i] == -1 || !isDirected && vec[i] == 1) {
        cnt++;
      }
    }
    stringstream << cnt << '\n';
    cnt = 0;
  }
  ostream << stringstream.str();
}

void IncidenceMatrix::EdgesCount(std::ostream &ostream) {
  std::ostringstream stringstream;
  stringstream << "Число " << (isDirected ? "дуг = " : "рёбер = ")
               << static_cast<int>(matrix.size()) << '\n';
  ostream << stringstream.str();
}
