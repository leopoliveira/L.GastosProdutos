const getEnumStrings = (enumObj: any): string[] => {
  return Object.keys(enumObj).filter((key) => isNaN(Number(key)));
};

export { getEnumStrings };
