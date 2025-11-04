import ICreatePacking from './ICreatePacking';

export default interface IUpdatePacking extends ICreatePacking {
  packingUnitPrice: number;
}
