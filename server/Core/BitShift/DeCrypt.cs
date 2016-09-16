namespace Core.BitShift
{
    public class Crypt
    {
        public int getId()
        {
            return this.GetHashCode() / 4096;
        }

        public int getShift()
        {
            return (this.getId() + 29890) % 7 + 1;
        }

        public byte[] DeCrypt(byte[] data)
        {
            byte num = data[data.Length - 1];
            for (int index = data.Length - 1; index > 0; --index)
                data[index] = (byte)(((int)data[index - 1] & (int)byte.MaxValue) << 8 - this.getShift() | ((int)data[index] & (int)byte.MaxValue) >> this.getShift());
            data[0] = (byte)((int)num << 8 - this.getShift() | ((int)data[0] & (int)byte.MaxValue) >> this.getShift());
            return data;
        }
    }
}
