import { UnitOfMeasure } from '@/common/enums/unit-of-measure.enum';
import IReadPacking from '@/common/interfaces/packing/IReadPacking';
import PackingService from '@/common/services/packing';
import { getEnumStrings } from '@/common/utils/utils';
import {
  Button,
  FormControl,
  FormLabel,
  Input,
  Modal,
  ModalBody,
  ModalCloseButton,
  ModalContent,
  ModalFooter,
  ModalHeader,
  ModalOverlay,
  Select,
} from '@chakra-ui/react';
import { useEffect, useState } from 'react';

type PackingModalProps = {
  isOpen: boolean;
  onClose: () => void;
  packing: IReadPacking | null;
  onSubmit: (acao: string) => void;
};

const PackingModal: React.FC<PackingModalProps> = ({ isOpen, onClose, packing, onSubmit }) => {
  const [formData, setFormData] = useState<IReadPacking>({
    id: '',
    name: '',
    quantity: 0,
    price: 0,
    unitOfMeasure: 0,
    packingUnitPrice: 0,
  });

  useEffect(() => {
    if (packing) {
      setFormData(packing);
    } else {
      setFormData({
        id: '',
        name: '',
        quantity: 0,
        price: 0,
        unitOfMeasure: 0,
        packingUnitPrice: 0,
      });
    }
  }, [packing]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (formData.id) {
      PackingService.UpdatePacking(formData.id, formData).then(() => {
        onSubmit('salvo');
      });
    } else {
      PackingService.CreatePacking(formData).then(() => {
        onSubmit('criado');
      });
    }
    onClose();
  };

  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>{formData.id ? 'Editar Emabalagem' : 'Adicionar Emabalagem'}</ModalHeader>
        <ModalCloseButton />
        <ModalBody>
          {formData.id && (
            <FormControl mb={4}>
              <Input name="id" value={formData.id} hidden={true} readOnly={true} variant="filled" />
            </FormControl>
          )}
          <FormControl mb={4}>
            <FormLabel>Nome</FormLabel>
            <Input name="name" value={formData.name} onChange={handleChange} />
          </FormControl>
          <FormControl mb={4}>
            <FormLabel>Quantidade</FormLabel>
            <Input
              name="quantity"
              type="number"
              value={formData.quantity}
              onChange={handleChange}
            />
          </FormControl>
          <FormControl mb={4}>
            <FormLabel>Preço</FormLabel>
            <Input name="price" type="number" value={formData.price} onChange={handleChange} />
          </FormControl>
          <FormControl mb={4}>
            <FormLabel>Unidade de Medida</FormLabel>
            <Select name="unitOfMeasure" value={formData.unitOfMeasure} onChange={handleChange}>
              {getEnumStrings(UnitOfMeasure).map((unit, index) => (
                <option key={index} value={UnitOfMeasure[unit as keyof typeof UnitOfMeasure]}>
                  {unit}
                </option>
              ))}
            </Select>
          </FormControl>
          <FormControl mb={4}>
            <FormLabel>Preço Unitário</FormLabel>
            <Input
              disabled={true}
              type="number"
              value={
                isNaN(formData.price / formData.quantity) ? 0 : formData.price / formData.quantity
              }
            />
          </FormControl>
        </ModalBody>
        <ModalFooter>
          <Button colorScheme="blue" mr={3} onClick={handleSubmit}>
            {formData.id ? 'Atualizar' : 'Criar'}
          </Button>
          <Button variant="ghost" onClick={onClose}>
            Cancelar
          </Button>
        </ModalFooter>
      </ModalContent>
    </Modal>
  );
};

export default PackingModal;
