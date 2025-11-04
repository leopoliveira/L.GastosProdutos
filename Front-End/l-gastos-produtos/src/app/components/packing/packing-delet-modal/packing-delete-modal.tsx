import PackingService from '@/common/services/packing';
import {
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalFooter,
  ModalBody,
  ModalCloseButton,
  Button,
} from '@chakra-ui/react';

type PackingDeleteModalProps = {
  phrase: string;
  btnConfirmLabel: string;
  btnCancelLabel: string;
  packingId: string;
  isOpen: boolean;
  onConfirm: () => void;
  onClose: () => void;
};

const PackingDeleteModal: React.FC<PackingDeleteModalProps> = ({
  phrase,
  btnConfirmLabel,
  btnCancelLabel,
  packingId,
  isOpen,
  onConfirm,
  onClose,
}) => {
  const handleConfirm = () => {
    PackingService.DeletePacking(packingId);
    onConfirm();
  };

  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>Atenção</ModalHeader>
        <ModalCloseButton />
        <ModalBody>{phrase}</ModalBody>
        <ModalFooter>
          <Button colorScheme="red" mr={3} onClick={handleConfirm}>
            {btnConfirmLabel}
          </Button>
          <Button variant="ghost" onClick={onClose}>
            {btnCancelLabel}
          </Button>
        </ModalFooter>
      </ModalContent>
    </Modal>
  );
};

export default PackingDeleteModal;
