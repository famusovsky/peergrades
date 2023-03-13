#include "MatrixInput.h"
#include <string>
#include <sstream>

std::vector<std::vector<int>> MatrixInput(std::istream & istream) {
  std::vector<std::vector<int>> res;
  while (true) {
    std::string inp;
    std::getline(istream, inp);
    std::istringstream stringstream(inp);
    if (inp != "End") {
      int num;
      std::vector<int> vec;
      while (stringstream >> num) {
        vec.push_back(num);
      }
      res.push_back(vec);
      if (istream.eof()) {
        break;
      }
    } else {
      break;
    }
  }
  return res;
}
