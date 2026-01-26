const getEnumStrings = (enumObj: any): string[] => {
  return Object.keys(enumObj).filter((key) => isNaN(Number(key)));
};

const normalizeString = (str: string): string => {
  return str
    .normalize('NFD')
    .replace(/[\u0300-\u036f]/g, '')
    .toLowerCase();
};

export { getEnumStrings, normalizeString };
