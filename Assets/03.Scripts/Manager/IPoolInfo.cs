
public interface IPoolInfo
{
    int TotalCount { get; }     // 지금까지 생성된 전체 개수
    int InactiveCount { get; }  // 풀 안에 비활성으로 대기 중인 개수
    int ActiveCount { get; }    // 사용 중인 개수
    int DisabledCount { get; }  // 비활성 카운터
}
